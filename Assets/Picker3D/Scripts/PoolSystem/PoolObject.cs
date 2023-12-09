using Picker3D.LevelSystem;
using UnityEngine;

namespace Picker3D.PoolSystem
{
    public abstract class PoolObject : MonoBehaviour
    {
        [SerializeField] protected GameObject visualObject;
        
        public abstract void Build();
    }
}
