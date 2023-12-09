using System;
using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.PoolSystem
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private PoolObject poolObjectPrefab;
        
        private readonly Queue<PoolObject> _poolObjects = new Queue<PoolObject>();

        public PoolObject GetNewObject(Action<PoolObject> completedAction)
        {
            completedAction += ReturnToQueue;
            if (_poolObjects.Count != 0) return _poolObjects.Dequeue();
            
            PoolObject newObject = Instantiate(poolObjectPrefab);
            _poolObjects.Enqueue(newObject);

            return _poolObjects.Dequeue();
        }

        private void ReturnToQueue(PoolObject poolObject)
        {
            _poolObjects.Enqueue(poolObject);
        }
    }
}
