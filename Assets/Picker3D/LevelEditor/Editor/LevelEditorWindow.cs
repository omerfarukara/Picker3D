using System;
using System.Collections.Generic;
using Picker3D.General;
using Picker3D.LevelSystem;
using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor.Editor
{
    public class LevelEditorWindow : EditorWindow
    {
        private static LevelEditorWindow instance;
        
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
        
        private int _currentLevel = 1;

        private List<StageData> _stagesData = new List<StageData>();

        #region Colors

        private readonly Color _emptyColor = new Color(0.22f, 0.21f, 0.24f);
        private readonly Color _cubeColor = new Color(0.53f, 0.02f, 0f);
        private readonly Color _cylinderColor = new Color(1f, 0.96f, 0.15f);
        private readonly Color _sphereColor = new Color(0.22f, 0.84f, 1f);
        private readonly Color _triangleColor = new Color(0.26f, 0.44f, 1f);

        #endregion

        #region For Toolbars

        private int _currentStage;
        private int _currentStageType;
        private int _currentCollectableType;
        private readonly List<string> _stagesToolbarOptions = new List<string>(); // Stages Toolbar Optinos

        private readonly string[] _stageTypeToolbarOptions = new string[]
        {
            StageType.NormalCollectable.ToString(), StageType.BigMultiplierCollectable.ToString(),
            StageType.Drone.ToString()
        };

        private readonly string[] _collectableTypeToolbarOptions = new string[]
        {
            CollectableType.Cube.ToString(), CollectableType.Cylinder.ToString(), CollectableType.Sphere.ToString(),
            CollectableType.Triangle.ToString()
        };

        #endregion

        private Vector2 _scrollPosition;
        private int _rowCount;
        private int _columnCount;
        
        public void ShowWindow(int level, bool isNewLevel)
        {
            if (!isNewLevel)
            {
                GetData();
            }
            
            _currentLevel = level;
            
            GetWindow<LevelEditorWindow>("Level Editor Window");
        }

        private void GetData()
        {
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(GameConstants.LevelDataPath + $"/Level{_currentLevel}.asset");
            
            LevelObjectData levelObjectData = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelObjectData>(assetPathAndName);

            _stagesData = levelObjectData.GetLevelData();
        }

        private void OnEnable()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

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

        private void DrawInformation()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Box($"Level : {_currentLevel}");
            GUILayout.Space(10);
            GUILayout.Box($"Editing Stage: {_currentStage + 1}");
            GUILayout.Space(10);
            GUILayout.Box($"Stage Count = {_stagesToolbarOptions.Count}");
            GUILayout.EndHorizontal();
        }

        private void DrawStageButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            if (GUILayout.Button("Add Stage", GUILayout.Height(30)))
            {
                _stagesToolbarOptions.Add($"{_stagesToolbarOptions.Count + 1}");

                StageData newStageData = new StageData
                {
                    StageIndex = _stagesData.Count > 0 ? _currentStage : 0,
                    StageType = (StageType)_currentStageType,
                    CollectableNodeData = new CollectableType[GameConstants.NormalColumnCount, GameConstants.NormalRowCount]
                };
                
                if (_stagesData.Count > 0)
                {
                    _stagesData[_currentStage].CollectableNodeData = new CollectableType[_columnCount, _rowCount];
                    _stagesData[_currentStage].StageType = (StageType)_currentStageType;
                }
                
                _stagesData.Add(newStageData);
                _rowCount = GameConstants.NormalRowCount;
                _columnCount = GameConstants.NormalColumnCount;
                _currentStage = _stagesToolbarOptions.Count - 1;
                _currentStageType = 0;
                _currentCollectableType = 0;
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Remove Stage", GUILayout.Height(30)))
            {
                
            }

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }

        private void DrawStageToolbar()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentStage, _stagesToolbarOptions.ToArray());

            if (toolbarStage != _currentStage)
            {
                // Yeni stage seçildi verileri kaydet!

                // Yeni stage verilileri gelicek
                StageData data = _stagesData[toolbarStage];
                
                if (_stagesData.Count > 0)
                {
                    _stagesData[_currentStage].CollectableNodeData = new CollectableType[_columnCount, _rowCount];
                    _stagesData[_currentStage].StageType = (StageType)toolbarStage;
                }

                _rowCount = data.CollectableNodeData.GetLength(1);
                _columnCount = data.CollectableNodeData.GetLength(0);
                _currentStageType = (int)data.StageType;
                _currentCollectableType = 0;

                DrawInformation();
            }

            _currentStage = toolbarStage;

            GUILayout.EndHorizontal();

            //DrawBoxes();
        }

        private void DrawStageType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentStageType, _stageTypeToolbarOptions);


            if (toolbarStage != _currentStageType)
            {
                // Yeni stage type seçildi

                switch ((StageType)toolbarStage + 1)
                {
                    case StageType.None:
                        break;
                    case StageType.NormalCollectable:
                        _rowCount = GameConstants.NormalColumnCount;
                        _columnCount = GameConstants.NormalColumnCount;
                        break;
                    case StageType.BigMultiplierCollectable:
                        _rowCount = GameConstants.BigRowCount;
                        _columnCount = GameConstants.BigColumnCount;
                        break;
                    case StageType.Drone:
                        _rowCount = 0;
                        _columnCount = 0;
                        break;
                }

                if (_stagesData.Count > 0)
                {
                    _stagesData[_currentStage].CollectableNodeData = new CollectableType[_columnCount, _rowCount];
                    _stagesData[_currentStage].StageType = (StageType)toolbarStage;
                }

                _currentStageType = toolbarStage;
            }

            GUILayout.EndHorizontal();
        }

        private void DrawCollectableType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentCollectableType, _collectableTypeToolbarOptions);

            if (toolbarStage != _currentCollectableType)
            {
                // Yeni collectable type seçildi
            }

            _currentCollectableType = toolbarStage;

            GUILayout.EndHorizontal();
        }

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

            for (int i = 0; i < _columnCount; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                for (int j = 0; j < _rowCount; j++)
                {
                    DrawColoredBox(i, j);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        private void DrawColoredBox(int column, int row)
        {
            int width = _currentStageType + 1 == (int)StageType.NormalCollectable ? 15 : 50;
            int height = _currentStageType + 1 == (int)StageType.NormalCollectable ? 15 : 50;

            Rect boxRect = GUILayoutUtility.GetRect(width, height, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));

            StageData stageData = _stagesData[_currentStage];

            Color color = stageData.CollectableNodeData[column, row] switch
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
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y + boxRect.height - 1, boxRect.width, 1),
                Color.black); // Bottom
            EditorGUI.DrawRect(new Rect(boxRect.x + boxRect.width - 1, boxRect.y, 1, boxRect.height),
                Color.black); // Right

            if (Event.current.type != EventType.MouseDown || !boxRect.Contains(Event.current.mousePosition)) return;

            if (stageData.CollectableNodeData[column, row] == (CollectableType)_currentCollectableType + 1)
            {
                stageData.CollectableNodeData[column, row] = CollectableType.None;
            }
            else
            {
                stageData.CollectableNodeData[column, row] = (CollectableType)_currentCollectableType + 1;
            }

            Repaint();
        }

        private void DrawLevelButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            
            if (GUILayout.Button("Save Level", GUILayout.MaxWidth(100), GUILayout.Height(30)))
            {
                LevelRecorder.SaveLevel(_currentLevel, _stagesData.ToArray());
                Close();
            }

            GUILayout.EndHorizontal();
        }
    }
}