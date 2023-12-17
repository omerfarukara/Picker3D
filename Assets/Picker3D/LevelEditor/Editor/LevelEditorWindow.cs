using System;
using System.Collections.Generic;
using System.Linq;
using Picker3D.General;
using Picker3D.LevelSystem;
using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor.Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        /// <summary>
        /// Instance field of LevelEditorWindow class
        /// </summary>
        private static LevelEditorWindow instance;
        
        /// <summary>
        /// Instance property of LevelEditorWindow class
        /// </summary>
        public static LevelEditorWindow Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<LevelEditorWindow>();
                }
                return instance;
            } 
        }

        #region Collectable Colors

        private readonly Color _emptyColor = new Color(0.22f, 0.21f, 0.24f);
        private readonly Color _cubeColor = new Color(0.53f, 0.02f, 0f);
        private readonly Color _cylinderColor = new Color(1f, 0.96f, 0.15f);
        private readonly Color _sphereColor = new Color(0.22f, 0.84f, 1f);
        private readonly Color _triangleColor = new Color(0.26f, 0.44f, 1f);

        #endregion
        
        #region For Toolbars
        
        /// <summary>
        /// Toolbar options for stages (1, 2, etc)
        /// </summary>
        private readonly List<string> _stagesToolbarOptions = new List<string>();
        /// <summary>
        /// Toolbar options for stage types
        /// </summary>
        private readonly string[] _stageTypeToolbarOptions =
        {
            StageType.NormalCollectable.ToString(), StageType.BigMultiplierCollectable.ToString(),
            StageType.Drone.ToString()
        };
        /// <summary>
        /// Toolbar options for collectable types
        /// </summary>
        private readonly string[] _collectableTypeToolbarOptions =
        {
            CollectableType.Cube.ToString(), CollectableType.Cylinder.ToString(), CollectableType.Sphere.ToString(),
            CollectableType.Triangle.ToString()
        };
        
        #endregion
        
        /// <summary>
        /// Data for stages in current level
        /// </summary>
        private List<StageData> _stagesData = new List<StageData>();

        /// <summary>
        /// Game object component in the editor scene to preview level items.
        /// </summary>
        private LevelPreview _levelPreview;
        
        /// <summary>
        /// Scroll view position value
        /// </summary>
        private Vector2 _scrollPosition;
        
        /// <summary>
        /// Currently editing level index
        /// </summary>
        private int _currentLevel = 1;
        /// <summary>
        /// Currently selected stage
        /// </summary>
        private int _currentStage;
        /// <summary>
        /// Currently selected stage type
        /// </summary>
        private int _currentStageType;
        /// <summary>
        /// Currently selected collectable type
        /// </summary>
        private int _currentCollectableType;
        
        /// <summary>
        /// Open the editor window
        /// </summary>
        /// <param name="level"></param>
        /// <param name="isNewLevel"></param>
        public void ShowWindow(int level, bool isNewLevel)
        {
            _currentLevel = level;

            if (!isNewLevel)
            {
                GetData();
            }
            
            GetWindow<LevelEditorWindow>("Level Editor Window");
        }
        
        /// <summary>
        /// Get the data in level object data
        /// </summary>
        private void GetData()
        {
            string assetPathAndName = $"{GameConstants.LevelDataPath}/Level{_currentLevel}.asset";
            
            LevelObjectData levelObjectData = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelObjectData>(assetPathAndName);

            _stagesData = levelObjectData.GetLevelData();
            
            for (int i = 0; i < _stagesData.Count; i++)
            {
                _stagesToolbarOptions.Add($"{i + 1}");
                _stagesData[i].StageType--;
            }

            if (_stagesData.Count== 0) return;
            
            _currentStageType = (int)_stagesData[0].StageType;
            
            SaveStageData();
        }

        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        /// <summary>
        /// Editor GUI handler (Unity event function)
        /// </summary>
        private void OnGUI()
        {
            DrawInformation();
            DrawStageButtons();
            DrawStageToolbar();
            DrawStageType();
            DrawCollectableType();
            DrawCollectableInformation();
            DrawLevelButtons();
            DrawBoxes();
        }

        /// <summary>
        /// Draws the information of panel
        /// </summary>
        private void DrawInformation()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Box($"Level : {_currentLevel}");
            if (_stagesToolbarOptions.Count != 0)
            {
                GUILayout.Space(10);
                GUILayout.Box($"Editing Stage: {_currentStage + 1}");
            }
            GUILayout.Space(10);
            GUILayout.Box($"Stage Count = {_stagesToolbarOptions.Count}");
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draws the stage stuff buttons and listen to GUI
        /// </summary>
        private void DrawStageButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            if (GUILayout.Button("Add Stage", GUILayout.Height(30)))
            {
                SaveStageData();
                
                _stagesToolbarOptions.Add($"{_stagesToolbarOptions.Count + 1}");
                _currentStage = _stagesToolbarOptions.Count -1;
                _currentStageType = 0;
                _currentCollectableType = 0;
                
                StageData newStageData = new StageData
                {
                    StageIndex = _currentStage,
                    StageType = (StageType)_currentStageType,
                    NormalCollectableNodeData = new CollectableType[GameConstants.NormalColumnCount, GameConstants.NormalRowCount]
                };
                
                _stagesData.Add(newStageData);
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Remove Stage", GUILayout.Height(30)))
            {
                
            }

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draws the stage toolbar and listen to GUI
        /// </summary>
        private void DrawStageToolbar()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentStage, _stagesToolbarOptions.ToArray());

            if (toolbarStage != _currentStage) // Different stage selected
            {
                StageData data = _stagesData[toolbarStage];
                
                SaveStageData();
                _currentStageType = (int)data.StageType;
                _currentCollectableType = 0;

                DrawInformation();
            }
            
            _currentStage = toolbarStage;
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draws the stage type toolbar and listen to GUI
        /// </summary>
        private void DrawStageType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentStageType, _stageTypeToolbarOptions);

            if (toolbarStage != _currentStageType) // Different stage type selected
            {
                SaveStageData();
            }
            _currentStageType = toolbarStage;

            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draws the collectable type toolbar and listen to GUI
        /// </summary>
        private void DrawCollectableType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentCollectableType, _collectableTypeToolbarOptions);

            _currentCollectableType = toolbarStage;

            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draws collectable information with colors
        /// </summary>
        private void DrawCollectableInformation()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(15, 5), _cubeColor);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(15, 5),_cylinderColor);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(15, 5), _sphereColor);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            EditorGUI.DrawRect(GUILayoutUtility.GetRect(15, 5), _triangleColor);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draws boxes for collectables in map
        /// </summary>
        private void DrawBoxes()
        {
            if (_stagesData.Count == 0) return;

            EditorGUI.BeginChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Repaint();
            }

            GUILayout.Space(5);

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            GUILayout.BeginVertical();

            if (_currentStageType + 1 == (int)StageType.NormalCollectable)
            {
                for (int i = 0; i < GameConstants.NormalColumnCount; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                
                    for (int j = 0; j < GameConstants.NormalRowCount; j++)
                    {
                        DrawNormalColoredBox(i, j);
                    }

                    GUILayout.EndHorizontal();
                }
            }
            else if(_currentStageType + 1 == (int)StageType.BigMultiplierCollectable)
            {
                for (int i = 0; i < GameConstants.BigColumnCount; i++)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(10);
                
                    for (int j = 0; j < GameConstants.BigRowCount; j++)
                    {
                        DrawBigColoredBox(i, j);
                    }

                    GUILayout.EndHorizontal();
                }
            }
            
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
        
        /// <summary>
        /// Draw one normal box and listen to GUI
        /// </summary>
        /// <param name="column"> column index </param>
        /// <param name="row"> row index </param>
        private void DrawNormalColoredBox(int column, int row)
        {
            const int width = 15;
            const int height = 15;

            Rect boxRect = GUILayoutUtility.GetRect(width, height, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

            StageData stageData = _stagesData[_currentStage];

            Color color = stageData.NormalCollectableNodeData[column, row] switch
            {
                CollectableType.None => _emptyColor,
                CollectableType.Cube => _cubeColor,
                CollectableType.Cylinder => _cylinderColor,
                CollectableType.Sphere => _sphereColor,
                CollectableType.Triangle => _triangleColor,
                _ => throw new ArgumentOutOfRangeException()
            };

            EditorGUI.DrawRect(boxRect, color);

            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, boxRect.width, 1), Color.black); // Top
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, 1, boxRect.height), Color.black); // Left
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y + boxRect.height - 1, boxRect.width, 1), Color.black); // Bottom
            EditorGUI.DrawRect(new Rect(boxRect.x + boxRect.width - 1, boxRect.y, 1, boxRect.height), Color.black); // Right

            if (Event.current.type != EventType.MouseDown || !boxRect.Contains(Event.current.mousePosition)) return;

            if (stageData.NormalCollectableNodeData[column, row] == (CollectableType)_currentCollectableType + 1)
            {
                stageData.NormalCollectableNodeData[column, row] = CollectableType.None;
            }
            else
            {
                stageData.NormalCollectableNodeData[column, row] = (CollectableType)_currentCollectableType + 1;
            }

            Repaint();
        }

        /// <summary>
        /// Draw one big box and listen to GUI
        /// </summary>
        /// <param name="column"> column index </param>
        /// <param name="row"> row index </param>
        private void DrawBigColoredBox(int column, int row)
        {
            const int width = 50;
            const int height = 50;

            Rect boxRect = GUILayoutUtility.GetRect(width, height, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

            StageData stageData = _stagesData[_currentStage];

            Color color = stageData.BigCollectableNodeData[column, row] switch
            {
                CollectableType.None => _emptyColor,
                CollectableType.Cube => _cubeColor,
                CollectableType.Cylinder => _cylinderColor,
                CollectableType.Sphere => _sphereColor,
                CollectableType.Triangle => _triangleColor,
                _ => throw new ArgumentOutOfRangeException()
            };

            EditorGUI.DrawRect(boxRect, color);

            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, boxRect.width, 1), Color.black); // Top
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, 1, boxRect.height), Color.black); // Left
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y + boxRect.height - 1, boxRect.width, 1), Color.black); // Bottom
            EditorGUI.DrawRect(new Rect(boxRect.x + boxRect.width - 1, boxRect.y, 1, boxRect.height), Color.black); // Right

            if (Event.current.type != EventType.MouseDown || !boxRect.Contains(Event.current.mousePosition)) return;

            if (stageData.BigCollectableNodeData[column, row] == (CollectableType)_currentCollectableType + 1)
            {
                stageData.BigCollectableNodeData[column, row] = CollectableType.None;
            }
            else
            {
                stageData.BigCollectableNodeData[column, row] = (CollectableType)_currentCollectableType + 1;
            }

            Repaint();
        }
        
        /// <summary>
        /// Draws the level buttons and listen to GUI
        /// </summary>
        private void DrawLevelButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            
            if (GUILayout.Button("Preview", GUILayout.MaxWidth(100), GUILayout.Height(30)))
            {
                if (_levelPreview == null)
                {
                    _levelPreview = GameObject.FindFirstObjectByType<LevelPreview>();
                }
                
                _levelPreview.Preview(_stagesData.ToArray());
            }

            GUILayout.Space(10);
            
            if (GUILayout.Button("Save Level", GUILayout.MaxWidth(100), GUILayout.Height(30)))
            {
                SaveStageData();
                LevelRecorder.SaveLevel(_currentLevel, _stagesData.ToArray());
                Close();
            }
            
            GUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Saves the stage data, within the entered data.
        /// </summary>
        private void SaveStageData()
        {
            if (_stagesData.Count == 0) return;
            
            _stagesData[_currentStage].StageType = (StageType)_currentStageType;
        }
    }
}