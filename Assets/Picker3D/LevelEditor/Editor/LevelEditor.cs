using System;
using Picker3D.General;
using Picker3D.LevelSystem;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Picker3D.LevelEditor.Editor
{
    public class LevelEditor : EditorWindow
    {
        private enum EditType
        {
            None = 0,
            NewLevel = 1,
            EditLevel = 2
        }

        private static EditType editType;
        private static int lastLevelIndex;
        
        private string _inputValue;
        private string _message;

        private static bool CheckInLevelEditorScene()
        {
            string currentSceneName = EditorSceneManager.GetActiveScene().name;

            return currentSceneName == GameConstants.LevelEditorSceneName;
        }
        
        [MenuItem("Level Editor / New Level")]
        private static void NewLevel()
        {
            editType = EditType.NewLevel;
            SetLevelIndex();
            LevelEditorWindow.Instance.ShowWindow(lastLevelIndex + 1, true);
        }
        
        [MenuItem("Level Editor / Edit Level")]
        private static void EditLevel()
        {
            editType = EditType.EditLevel;
            SetLevelIndex();
            GetWindow<LevelEditor>("Level Editor").Show();
        }

        private static void SetLevelIndex()
        {
            string levelContentDataAssetPath = $"{GameConstants.LevelDataPath}/LevelContentData.asset";
            LevelContentData levelContentData = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelContentData>(levelContentDataAssetPath);
            lastLevelIndex = levelContentData.LevelCount;
        }
        
        private void OnGUI()
        {
            if (!CheckInLevelEditorScene())
            {
                if (GUILayout.Button("Go to Editor Scene", GUILayout.Height(30)))
                {
                    EditorSceneManager.OpenScene($"Assets/Picker3D/LevelEditor/{GameConstants.LevelEditorSceneName}.unity");
                    Close();
                }
                return;
            }

            if (editType != EditType.EditLevel) return;

            if (lastLevelIndex == 0)
            {
                GUILayout.Label($"No registered level data was found!");
                if (GUILayout.Button("New Level", GUILayout.MaxWidth(100)))
                {
                    editType = EditType.NewLevel;
                    SetLevelIndex();
                    LevelEditorWindow.Instance.ShowWindow(lastLevelIndex + 1, true);
                }
                return;
            }
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Level", EditorStyles.boldLabel);
            _inputValue = GUILayout.TextField(_inputValue, GUILayout.MinWidth(300));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Edit Level"))
            {
                int level = Convert.ToInt16(_inputValue);
                
                if (level > 0 && level <= lastLevelIndex)
                {
                    LevelEditorWindow.Instance.ShowWindow(level, false);
                    Close();
                }
                else
                {
                    _inputValue = "";
                    _message = $"The entered level was not found. Current level range: 1 - {lastLevelIndex}.";
                }
            }
            
            GUILayout.EndHorizontal();
            GUILayout.Label(_message);

        }
    }
}
