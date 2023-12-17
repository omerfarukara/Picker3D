using System;
using Picker3D.General;
using Picker3D.LevelSystem;
using Picker3D.Player;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using Picker3D.StageObjets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.StageObjects
{
    public class BigMultiplierCollectable : BaseCollectableObject
    {
        [SerializeField] private BigMultiplierObject[] bigVisualObjects;
        [SerializeField] private float fallDistance;
        
        [SerializeField] internal int minFragment;
        [SerializeField] internal int maxFragment;

        protected override void OnDisable()
        {
            base.OnDisable();
            Rigidbody.useGravity = false;
        }

        private void Update()
        {
            if (!Rigidbody.useGravity &&
                transform.position.z - PlayerController.Instance.transform.position.z <
                fallDistance)
            {
                Rigidbody.useGravity = true;
            }
        }

        protected override void OnBuild()
        {
            foreach (BigMultiplierObject bigVisualObject in bigVisualObjects)
            {
                if (bigVisualObject.CollectableType == CollectableType)
                {
                    bigVisualObject.gameObject.SetActive(true);
                    Rigidbody.useGravity = false;
                    Rigidbody.velocity = Vector3.zero;
                    Rigidbody.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}