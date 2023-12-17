using System;
using Picker3D.General;
using Picker3D.LevelSystem;
using Picker3D.Player;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Picker3D.StageObjects
{
    public class BigMultiplierCollectable : BaseCollectableObject
    {
        [SerializeField] private float fallDistance;
        
        [SerializeField] internal int minFragment;
        [SerializeField] internal int maxFragment;

        private void Update()
        {
            if (!Rigidbody.useGravity &&
                transform.position.z - PlayerController.Instance.transform.position.z <
                fallDistance)
            {
                Rigidbody.useGravity = true;
            }
        }

        public override void FixedUpdate()
        {
            if (!Rigidbody.useGravity) return;
            
            base.FixedUpdate();
        }
    }
}