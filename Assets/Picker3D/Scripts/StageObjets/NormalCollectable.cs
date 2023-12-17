using System.Collections.Generic;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using UnityEngine;

namespace Picker3D.StageObjects
{
    public class NormalCollectable : BaseCollectableObject
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float gravityScale;
        [SerializeField] private float force;

        private void FixedUpdate()
        {
            if (Rigidbody == null) return;
            Vector3 velocity = Rigidbody.velocity;
            velocity.y -= gravityScale;
            Rigidbody.velocity = velocity;
        }

        public override void Explode()
        {
            base.Explode();
        }

        public void Throw()
        {
            Rigidbody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
            IsThrow = true;
            gravityScale *= 2;

        }

        public void ColorChange(Material material)
        {
            meshRenderer.sharedMaterial = material;
        }

    }
}