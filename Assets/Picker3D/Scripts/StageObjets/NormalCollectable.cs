using Picker3D.Scripts.StageObjets;
using UnityEngine;

namespace Picker3D.StageObjects
{
    public class NormalCollectable : BaseCollectableObject
    {
        [SerializeField] private float force;

        protected override void OnDisable()
        {
            IsThrow = false;
        }
        
        public void Throw()
        {
            Rigidbody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
            IsThrow = true;
        }

        protected override void OnBuild()
        {
            
        }
    }
}