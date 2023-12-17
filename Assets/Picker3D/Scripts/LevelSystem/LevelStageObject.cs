using System;
using Picker3D.PoolSystem;
using Picker3D.StageObjets;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class LevelStageObject : MonoBehaviour
    {
        [SerializeField] private Transform collectableParent;
        [SerializeField] private StageController stageController;

        public void Build(LevelStageObjectData levelStageObjectData, int index)
        {
            transform.position = Vector3.forward * index * 65;
            switch (levelStageObjectData.StageType)
            {
                case StageType.None:
                    break;
                case StageType.NormalCollectable:
                    NormalCollectableBuild(levelStageObjectData);
                    break;
                case StageType.BigMultiplierCollectable:
                    BigCollectableBuild(levelStageObjectData);
                    break;
                case StageType.Drone:
                    break;
            }

            CollectableParentProcess();

            stageController.Initialize();
            stageController.RequiredCollectableCount = levelStageObjectData.RequiredCollectableCount();
        }

        private void CollectableParentProcess()
        {
            collectableParent.transform.localPosition = Vector3.up * -0.75f;
            collectableParent.transform.localScale = new Vector3(-1, 1, 1);
            collectableParent.transform.localRotation = Quaternion.Euler(Vector3.up * 180);
        }

        private void NormalCollectableBuild(LevelStageObjectData levelStageObjectData)
        {
            for (int i = 0; i < levelStageObjectData.CollectableCount(); i++)
            {
                PoolObject poolObject = PoolManager.Instance.GetPoolObject(levelStageObjectData.StageType);

                Vector3 newPosition = new Vector3(levelStageObjectData.Positions[i].x / 2 - 4.75f,
                    levelStageObjectData.Positions[i].y,
                    levelStageObjectData.Positions[i].z / 2);

                poolObject.transform.parent = collectableParent;
                poolObject.transform.position = newPosition;
                poolObject.CollectableType = levelStageObjectData.CollectableTypes[i];
                poolObject.Build();
            }
        }

        private void BigCollectableBuild(LevelStageObjectData levelStageObjectData)
        {
            for (int i = 0; i < levelStageObjectData.CollectableCount(); i++)
            {
                PoolObject poolObject = PoolManager.Instance.GetPoolObject(levelStageObjectData.StageType);


                Vector3 newPosition = new Vector3(levelStageObjectData.Positions[i].x * 3 - 3f,
                    levelStageObjectData.Positions[i].y,
                    levelStageObjectData.Positions[i].z * 3);

                poolObject.transform.parent = collectableParent;
                poolObject.transform.position = newPosition;
                poolObject.CollectableType = levelStageObjectData.CollectableTypes[i];
                poolObject.Build();
            }
        }
    }
}