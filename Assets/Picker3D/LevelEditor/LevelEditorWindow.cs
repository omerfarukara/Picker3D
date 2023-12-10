using System;
using System.Collections.Generic;
using Picker3D.LevelSystem;
using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelEditorWindow : EditorWindow
    {
        private const int NormalRowCount = 20;
        private const int NormalColumnCount = 80;
        private const int BigRowCount = 3;
        private const int BigColumnCount = 12;
        private List<StageData> _stagesData = new List<StageData>();
        
        #region Colors

        private readonly Color _cubeColor = new Color(0f, 1f, 0.14f);
        private readonly Color _sphereColor = new Color(0.22f, 0.84f, 1f);
        private readonly Color _triangleColor = new Color(0.26f, 0.44f, 1f);
        private readonly Color _cylinderColor = new Color(1f, 0.96f, 0.15f);
        private readonly Color _emptyColor = new Color(0.22f, 0.21f, 0.24f);

        #endregion
        
        private readonly List<string> _stages = new List<string>();
        private int _currentStage;

        #region For Toolbars

        private int _currentStageType;
        private readonly string[] _stageTypes = new string[] {StageType.NormalCollectable.ToString(), StageType.BigMultiplierCollectable.ToString(), StageType.Drone.ToString()};
        private int _currentCollectableType;
        private readonly string[] _collectableTypes = new string[] {CollectableType.Cube.ToString(), CollectableType.Cylinder.ToString(), CollectableType.Sphere.ToString(), CollectableType.Triangle.ToString()};

        #endregion
        
        private Vector2 _scrollPosition;
        
        private int _rowCount;
        private int _columnCount;

        // Datadan çekilecekler
        private int _currentLevel;
        private int StageCount => _stages.Count;

        GUIStyle _buttonStyle;

        [MenuItem("Window/Level Editor")]
        public static void ShowWindow(int level)
        {
            GetWindow<LevelEditorWindow>("Level Editor Window");
        }

        private void OnGUI()
        {
            DrawInformation();
            DrawStageButtons();
            DrawStageToolbar();
            DrawStageType();
            DrawCollectableType();
            DrawLevelButtons();
            DrawBoxes();
        }

        private void OnFocus()
        {
            //collectableTypes = new CollectableType[ColumnCount, RowCount];
        }
        
        private void DrawInformation()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.Box($"New Level : {_currentLevel}");
            GUILayout.Space(10);
            GUILayout.Box($"Editing Stage: {_currentStage + 1}");
            GUILayout.Space(10);
            GUILayout.Box($"Stage Count = {StageCount}");
            GUILayout.EndHorizontal();
        }

        private void DrawStageButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            if (GUILayout.Button("Add Stage", GUILayout.Height(30)))
            {
                Debug.Log("Butona tıklandı!");
                _stages.Add($"{_stages.Count + 1}");
                
                switch ((StageType)_currentStageType + 1)
                {
                    case StageType.None:
                        _rowCount = 0;
                        _columnCount = 0;
                        break;
                    case StageType.NormalCollectable:
                        _rowCount = NormalRowCount;
                        _columnCount = NormalColumnCount;
                        break;
                    case StageType.BigMultiplierCollectable:
                        _rowCount = BigRowCount;
                        _columnCount = BigColumnCount;
                        break;
                    case StageType.Drone:
                        _rowCount = 0;
                        _columnCount = 0;
                        break;
                }
                
                StageData stageData = new StageData
                {
                    StageIndex = _stages.Count + 1,
                    StageType = (StageType)_currentStage + 1,
                    CollectableTypes = new CollectableType[_columnCount, _rowCount]
                };
                
                _stagesData.Add(stageData);
                DrawInformation();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Remove Stage", GUILayout.Height(30)))
            {
                Debug.Log("Butona tıklandı!");
                _stages.Remove($"{_stages.Count}");
            }

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }

        private void DrawLevelButtons()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Save Level", GUILayout.MaxWidth(100), GUILayout.Height(30)))
            {
                Close();
            }
            GUILayout.EndHorizontal();
        }

        private void DrawStageToolbar()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);

            int toolbarStage = GUILayout.Toolbar(_currentStage, _stages.ToArray());
            
            if (toolbarStage != _currentStage)
            {
                // Yeni stage seçildi verileri kaydet!
                
                DrawInformation();
                SaveStage(_currentStage);
                _currentStageType = (int)_stagesData[toolbarStage].StageType;
            }

            _currentStage = toolbarStage;

            GUILayout.EndHorizontal();
        }

        private void ResetStageData(int stageIndex)
        {
            switch ((StageType)stageIndex)
            {
                case StageType.None:
                    _rowCount = 0;
                    _columnCount = 0;
                    break;
                case StageType.NormalCollectable:
                    _rowCount = NormalRowCount;
                    _columnCount = NormalColumnCount;
                    break;
                case StageType.BigMultiplierCollectable:
                    _rowCount = BigRowCount;
                    _columnCount = BigColumnCount;
                    break;
                case StageType.Drone:
                    _rowCount = 0;
                    _columnCount = 0;
                    break;
            }

            _stagesData[_currentStage].CollectableTypes = new CollectableType[_columnCount, _rowCount];
            _stagesData[_currentStage].StageType = (StageType)stageIndex;
        }
        
        private void DrawStageType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            
            int toolbarStage = GUILayout.Toolbar(_currentStageType, _stageTypes);

            if (toolbarStage != _currentStageType)
            {
                // Yeni stage type seçildi
                ResetStageData(toolbarStage + 1);
            }
            
            _currentStageType = toolbarStage;
            
            GUILayout.EndHorizontal();
        }
        
        private void DrawCollectableType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            
            int toolbarStage = GUILayout.Toolbar(_currentCollectableType, _collectableTypes);

            if (toolbarStage != _currentCollectableType)
            {
                // Yeni stage type seçildi
            }
            
            _currentCollectableType = toolbarStage;
            
            GUILayout.EndHorizontal();
        }

        private void SaveStage(int stage)
        {
            Debug.Log($"{stage + 1} nolu stage kaydedildi");
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
                    DrawColoredBox( i ,j);
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
            
            Color color = stageData.CollectableTypes[column, row] switch
            {
                CollectableType.None => _emptyColor,
                CollectableType.Cube => _cubeColor,
                CollectableType.Sphere => _sphereColor,
                CollectableType.Cylinder => _cylinderColor,
                CollectableType.Triangle => _triangleColor
            };
            
            EditorGUI.DrawRect(boxRect, color);

            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, boxRect.width, 1), Color.black); // Top
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y, 1, boxRect.height), Color.black); // Left
            EditorGUI.DrawRect(new Rect(boxRect.x, boxRect.y + boxRect.height - 1, boxRect.width, 1), Color.black); // Bottom
            EditorGUI.DrawRect(new Rect(boxRect.x + boxRect.width - 1, boxRect.y, 1, boxRect.height), Color.black); // Right
            
            if (Event.current.type != EventType.MouseDown || !boxRect.Contains(Event.current.mousePosition)) return;

            if (stageData.CollectableTypes[column, row] == (CollectableType)_currentCollectableType + 1)
            {
                stageData.CollectableTypes[column, row] = 0;
            }
            else
            {
                stageData.CollectableTypes[column, row] = (CollectableType)_currentCollectableType + 1;
            }
            Repaint();
        }
    }

    public class StageData
    {
        public CollectableType[,] CollectableTypes { get; set; }
        public StageType StageType { get; set; }
        public int StageIndex { get; set; }
    }
}