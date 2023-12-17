using System;
using Picker3D.LevelSystem;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.PoolSystem
{
    public abstract class PoolObject : MonoBehaviour
    {
        [SerializeField] protected StageType stageType;
        
        public CollectableType CollectableType { get; set; }

        public StageType StageType => stageType;

        private void OnEnable()
        {
            UIManager.OnNextLevelButtonClicked += ReturnToPool;
            UIManager.OnRestartLevelButtonClicked += ReturnToPool;
        }

        private void OnDisable()
        {
            UIManager.OnNextLevelButtonClicked -= ReturnToPool;
            UIManager.OnRestartLevelButtonClicked -= ReturnToPool;
        }

        private void ReturnToPool()
        {
            PoolManager.Instance.ReturnToPool(this);
        }

        public abstract void Build();
        public abstract void CloseObject();
    }
}