using System;
using Picker3D.PoolSystem;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class LevelStageObject : MonoBehaviour
    {
        public void Build(LevelStageObjectData levelStageObjectData)
        {
            for (int i = 0; i < levelStageObjectData.CollectableCount; i++)
            {
                PoolObject poolObject = PoolManager.Instance.GetPoolObject(levelStageObjectData.StageType);

                poolObject.transform.position = levelStageObjectData.Positions[i];
                poolObject.Build();
            }
        }
    }
}
