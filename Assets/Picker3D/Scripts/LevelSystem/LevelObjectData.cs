using System.Collections.Generic;
using System.Linq;
using Picker3D.LevelEditor;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelObject01", menuName = "Game Data/Level Object Data")]
    public class LevelObjectData : ScriptableObject
    {
       [SerializeField] internal LevelStageObjectData[] levelStagesData;

       public LevelStageObjectData[] LevelStageObjectsData => levelStagesData;

       public void SetLevelData(StageData[] stagesData)
       {
           levelStagesData =  new LevelStageObjectData[stagesData.Length];

           for (int i = 0; i < stagesData.Length; i++)
           {
               Debug.Log($"Data {i} : {stagesData[i].StageType}");
               levelStagesData[i] = new LevelStageObjectData();
               levelStagesData[i].SetData(stagesData[i]);
           }
       }

       public List<StageData> GetLevelData()
       {
           List<StageData> stagesData = new List<StageData>();

           for (int i = 0; i < levelStagesData.Length; i++)
           {
               stagesData.Add(levelStagesData[i].GetStageData(i));
           }

           return stagesData;
       }
    }
}
