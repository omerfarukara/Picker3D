using UnityEngine;

namespace Picker3D.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelObject01", menuName = "Game Data/Level Object Data")]
    public class LevelObjectData : ScriptableObject
    {
       [SerializeField] private LevelStageObjectData[] levelStagesData;

       public int StageCount => levelStagesData.Length;
       public LevelStageObjectData[] LevelStageObjectsData => levelStagesData;
    }
}
