using System;
using Picker3D.LevelSystem;
using Picker3D.PoolSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.StageObjects
{
    public class BigMultiplierCollectable : BaseCollectableObject
    {
        [SerializeField] private int minFragment;
        [SerializeField] private int maxFragment;
        
        public override void Build()
        {
            base.Build();
            //Rigidbody.useGravity = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Ground"))
            {
                Multiply();
            }
        }

        private void Multiply()
        {
            int fragment = Random.Range(minFragment, maxFragment);

            for (int i = 0; i < fragment; i++)
            {
                PoolObject normalPoolObject = PoolManager.Instance.GetPoolObject(StageType.NormalCollectable);
                float localScale = visualObject.transform.localScale.x;
                float positionX = Random.Range(-localScale, localScale);
                float positionZ = Random.Range(-localScale, localScale);
                normalPoolObject.transform.position = transform.position + new Vector3(positionX, 0 , positionZ);
                normalPoolObject.Build();
            }
        }
    }
}
