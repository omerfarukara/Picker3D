using System;
using Picker3D.LevelSystem;
using Picker3D.Managers;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.PoolSystem
{
    public abstract class PoolObject : MonoBehaviour
    {
        [SerializeField] protected StageType stageType;
        
        public CollectableType CollectableType { get; set; }

        public StageType StageType => stageType;

        protected virtual void OnEnable()
        {
            GameManager.OnCompleteLevel += OnCompleteLevelHandler;
        }

        protected virtual void OnDisable()
        {
            GameManager.OnCompleteLevel += OnCompleteLevelHandler;
        }

        private void ReturnToPool()
        {
            gameObject.SetActive(false);
            PoolManager.Instance.ReturnToPool(this);
        }
        
        private void OnCompleteLevelHandler()
        {
            ReturnToPool();
        }

        public abstract void Build();
        public abstract void CloseObject();
    }
}