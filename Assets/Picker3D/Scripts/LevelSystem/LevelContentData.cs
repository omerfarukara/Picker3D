using UnityEngine;

namespace Picker3D.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelContentData", menuName = "Game Data/Level Content Data")]
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
