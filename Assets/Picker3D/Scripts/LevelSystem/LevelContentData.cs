using UnityEngine;

namespace Picker3D.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelContentData", menuName = "Game Data/Level Content Data")]
    public class LevelContentData : ScriptableObject
    {
        [SerializeField] internal LevelObjectData[] levelObjectsData;

        public int LevelCount => levelObjectsData.Length;

        /// <summary>
        /// Returns level object data in the current level
        /// </summary>
        public LevelObjectData GetLevelObjectData(int level)
        {
            return levelObjectsData[level];
        }
        
        /// <summary>
        /// Add new level in game
        /// </summary>
        public void AddLevelObject(LevelObjectData levelObject)
        {
            LevelObjectData[] newLevelObjectsData = new LevelObjectData[levelObjectsData.Length + 1];

            for (int i = 0; i < levelObjectsData.Length; i++)
            {
                newLevelObjectsData[i] = levelObjectsData[i];
            }

            newLevelObjectsData[^1] = levelObject;

            levelObjectsData = newLevelObjectsData;
        }
    }
}
