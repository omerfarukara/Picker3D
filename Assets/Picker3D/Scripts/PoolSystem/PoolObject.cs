using System;
using Picker3D.LevelSystem;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.PoolSystem
{
    public abstract class PoolObject : MonoBehaviour
    {
        [SerializeField] protected StageType stageType;
        [SerializeField] protected GameObject visualObject;
        [SerializeField] private float force;

        private Rigidbody _rigidbody;
        internal bool isThrow;
        
        public StageType StageType => stageType;

        private void OnEnable()
        {
            UIManager.OnNextLevelButtonClicked += ReturnToPool;
            UIManager.OnRestartLevelButtonClicked += ReturnToPool;
        }

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
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

        public void Throw()
        {
            _rigidbody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
            isThrow = true;
        }

        public abstract void Build();
        public abstract void CloseObject();
    }
}