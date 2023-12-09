using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class LevelContentData : ScriptableObject
    {
        [SerializeField] private LevelObjectData[] levelObjectsData;

        public int LevelCount => levelObjectsData.Length;

        public LevelObjectData GetLevelObjectData(int level)
        {
            return levelObjectsData[level];
        }
    }
}
