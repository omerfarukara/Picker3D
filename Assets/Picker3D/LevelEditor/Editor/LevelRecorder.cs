using Picker3D.LevelSystem;
using UnityEditor;
using UnityEngine;
using Picker3D.General;

namespace Picker3D.LevelEditor.Editor
{
    public abstract class LevelRecorder
    {
        public static void SaveLevel(int level, StageData[] stageData)
        {
            string levelContentDataAssetPath = $"{GameConstants.LevelDataPath}/LevelContentData.asset";
            LevelContentData levelContentData = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelContentData>(levelContentDataAssetPath);

            LevelObjectData levelObjectData;
            
            if (level > levelContentData.LevelCount) // New Level
            {
                levelObjectData = ScriptableObject.CreateInstance<LevelObjectData>();
                string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(GameConstants.LevelDataPath + $"/Level{level}.asset");
                AssetDatabase.CreateAsset(levelObjectData, assetPathAndName);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                levelContentData.AddLevelObject(levelObjectData);
            }
            else // Edit Level
            {
                string levelObjectDataAssetPath = $"{GameConstants.LevelDataPath}/Level{level}.asset";
                levelObjectData = UnityEditor.AssetDatabase.LoadAssetAtPath<LevelObjectData>(levelObjectDataAssetPath);
            }
            
            levelObjectData.SetLevelData(stageData);

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = levelObjectData;
        }
    }
}
