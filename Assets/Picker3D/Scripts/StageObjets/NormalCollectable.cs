using UnityEngine;

namespace Picker3D.StageObjects
{
    public class NormalCollectable : BaseCollectableObject
    {
        [SerializeField] private Vector3 forceDirection;
        [SerializeField] private float force;

        protected override void OnDisable()
        {
            IsThrow = false;
        }
        
        public void Throw()
        {
            Rigidbody.AddForce(forceDirection * force, ForceMode.VelocityChange);
            IsThrow = true;
        }

        protected override void OnBuild()
        {
            
        }
    }
}