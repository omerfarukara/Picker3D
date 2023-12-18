using Picker3D.General;
using Picker3D.LevelSystem;
using Picker3D.PoolSystem;
using Picker3D.StageObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.StageObjets
{
    public class BigMultiplierObject : MonoBehaviour
    {
        [SerializeField] private CollectableType collectableType;
        
        private BigMultiplierCollectable _bigMultiplierCollectable;
        private int _minFragment, _maxFragment;

        public CollectableType CollectableType => collectableType;

        private void OnEnable()
        {
            _bigMultiplierCollectable = GetComponentInParent<BigMultiplierCollectable>();
            _minFragment = _bigMultiplierCollectable.minFragment;
            _maxFragment = _bigMultiplierCollectable.maxFragment;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag(GameConstants.Ground))
            {
                Multiply();
            }
        }

        /// <summary>
        /// The big multiplier object closes and normal objects with the same collectable type spawn.
        /// </summary>
        private void Multiply()
        {
            int fragment = Random.Range(_minFragment, _maxFragment);

            for (int i = 0; i < fragment; i++)
            {
                PoolObject normalPoolObject = PoolManager.Instance.GetPoolObject(StageType.NormalCollectable);
                normalPoolObject.CollectableType = _bigMultiplierCollectable.CollectableType;
                float localScale = normalPoolObject.transform.localScale.x;
                float positionX = Random.Range(-localScale, localScale);
                float positionZ = Random.Range(-localScale, localScale);
                normalPoolObject.transform.position = transform.position + new Vector3(positionX, 0, positionZ);
                normalPoolObject.gameObject.SetActive(true);
                normalPoolObject.Build();
            }

            gameObject.SetActive(false);
        }
    }
}