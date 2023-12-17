using System;
using System.Collections.Generic;
using Picker3D.LevelSystem;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using UnityEngine;

namespace Picker3D.StageObjects
{
    public class NormalCollectable : BaseCollectableObject
    {
        [SerializeField] private float force;

        public void Throw()
        {
            Rigidbody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
            IsThrow = true;
            GravityScale *= 2;
        }

        public void ColorChange(Material material)
        {
            foreach (VisualStageObject visual in visualObjects)
            {
                if (visual.enabled)
                {
                    visual.GetComponent<MeshRenderer>().sharedMaterial = material;
                }
            }
        }
    }
}