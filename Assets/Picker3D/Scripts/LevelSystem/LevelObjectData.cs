using System.Collections.Generic;
using Picker3D.LevelEditor;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    [CreateAssetMenu(fileName = "LevelObject01", menuName = "Game Data/Level Object Data")]
    public class LevelObjectData : ScriptableObject
    {
       [SerializeField] internal LevelStageObjectData[] levelStagesData;

       public LevelStageObjectData[] LevelStageObjectsData => levelStagesData;

       /// <summary>
       /// Level data is set according to the editor
       /// </summary>
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

       /// <summary>
       /// Level data is get according to the editor
       /// </summary>
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
