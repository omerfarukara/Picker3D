using System;
using Picker3D.Managers;
using Picker3D.PoolSystem;
using Picker3D.StageObjets;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class LevelStageObject : MonoBehaviour
    {
        [SerializeField] private Transform mainGround;
        [SerializeField] private StageController stageController;

        private Action<LevelStageObject> _onComplete;
        private Transform _collectableParent;

        private void OnEnable()
        {
            GameManager.OnCompleteLevel += OnCompleteLevelHandler;
            UIManager.OnNextLevelButtonClicked += OnLevelChangeHandler;
            UIManager.OnRestartLevelButtonClicked += OnLevelChangeHandler;
        }

        private void OnDisable()
        {
            GameManager.OnCompleteLevel -= OnCompleteLevelHandler;
            UIManager.OnNextLevelButtonClicked -= OnLevelChangeHandler;
            UIManager.OnRestartLevelButtonClicked -= OnLevelChangeHandler;
        }

        public void Build(LevelStageObjectData levelStageObjectData, int index, Action<LevelStageObject> onComplete)
        {
            _onComplete = onComplete;

            transform.position = Vector3.forward * index * 65;
            switch (levelStageObjectData.StageType)
            {
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

        /// <summary>
        /// It makes the necessary adjustments so that the collectables lined up on the stage appear properly.
        /// </summary>
        private void CollectableParentProcess()
        {
            _collectableParent.parent = mainGround;
            _collectableParent.localPosition = Vector3.up * 0.75f;
            Vector3 localScale = _collectableParent.localScale;
            localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            _collectableParent.localScale = localScale;
            _collectableParent.localRotation = Quaternion.Euler(Vector3.up * 180);
        }

        /// <summary>
        /// Normal collectable processing
        /// </summary>
        private void NormalCollectableBuild(LevelStageObjectData levelStageObjectData)
        {
            for (int i = 0; i < levelStageObjectData.CollectableCount(); i++)
            {
                PoolObject poolObject = PoolManager.Instance.GetPoolObject(levelStageObjectData.StageType);

                Vector3 newPosition = new Vector3(levelStageObjectData.Positions[i].x / 2 - 4.75f,
                    levelStageObjectData.Positions[i].y,
                    levelStageObjectData.Positions[i].z / 2);

                if (_collectableParent == null)
                {
                    _collectableParent = new GameObject
                    {
                        name = "CollectableParent",
                    }.transform;
                }

                _collectableParent.parent = null;
                _collectableParent.position = Vector3.zero;
                _collectableParent.rotation = Quaternion.identity;

                poolObject.transform.position = newPosition;
                poolObject.transform.parent = _collectableParent;
                poolObject.CollectableType = levelStageObjectData.CollectableTypes[i];
                poolObject.gameObject.SetActive(true);
                poolObject.Build();
            }
        }

        /// <summary>
        /// Big collectable processing
        /// </summary>
        private void BigCollectableBuild(LevelStageObjectData levelStageObjectData)
        {
            for (int i = 0; i < levelStageObjectData.CollectableCount(); i++)
            {
                PoolObject poolObject = PoolManager.Instance.GetPoolObject(levelStageObjectData.StageType);

                Vector3 newPosition = new Vector3(levelStageObjectData.Positions[i].x * 3 - 3f,
                    levelStageObjectData.Positions[i].y,
                    levelStageObjectData.Positions[i].z * 3);

                if (_collectableParent == null)
                {
                    _collectableParent = new GameObject
                    {
                        name = "CollectableParent",
                    }.transform;
                }

                _collectableParent.parent = null;
                _collectableParent.position = Vector3.zero;
                _collectableParent.rotation = Quaternion.identity;

                poolObject.transform.position = newPosition;
                poolObject.transform.parent = _collectableParent;
                poolObject.CollectableType = levelStageObjectData.CollectableTypes[i];
                poolObject.gameObject.SetActive(true);
                poolObject.Build();
            }
        }

        private void OnCompleteLevelHandler()
        {
            _onComplete?.Invoke(this);
        }

        private void OnLevelChangeHandler()
        {
            stageController.ResetStage();
        }
    }
}