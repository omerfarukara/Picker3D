using Picker3D.PoolSystem;
using UnityEngine;

namespace Picker3D.StageObjects
{
    public class NormalCollectable : PoolObject
    {
        public override void Build()
        {
            visualObject.SetActive(true);
        }
    }
}
