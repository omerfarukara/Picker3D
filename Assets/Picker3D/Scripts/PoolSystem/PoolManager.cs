using System;
using System.Collections.Generic;
using System.ComponentModel;
using Picker3D.LevelSystem;
using Picker3D.Scripts.Helpers;
using UnityEngine;

namespace Picker3D.PoolSystem
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] private PoolObject normalCollectablePrefab;
        [SerializeField] private PoolObject bigCollectablePrefab;
        [SerializeField] private PoolObject dronePrefab;

        private readonly Queue<PoolObject> _poolNormalCollectableObjects = new Queue<PoolObject>();
        private readonly Queue<PoolObject> _poolBigCollectableObjects = new Queue<PoolObject>();
        private readonly Queue<PoolObject> _poolDroneObjects = new Queue<PoolObject>();

        public PoolObject GetPoolObject(StageType stageType)
        {
            switch (stageType)
            {
                case StageType.NormalCollectable:
                    if (_poolNormalCollectableObjects.Count != 0) return _poolNormalCollectableObjects.Dequeue();
            
                    PoolObject newNormalObject = Instantiate(normalCollectablePrefab);
                    _poolNormalCollectableObjects.Enqueue(newNormalObject);

                    return _poolNormalCollectableObjects.Dequeue();
                case StageType.BigMultiplierCollectable:
                    if (_poolBigCollectableObjects.Count != 0) return _poolBigCollectableObjects.Dequeue();
            
                    PoolObject newBigObject = Instantiate(bigCollectablePrefab);
                    _poolBigCollectableObjects.Enqueue(newBigObject);

                    return _poolBigCollectableObjects.Dequeue();
                case StageType.Drone:
                    if (_poolDroneObjects.Count != 0) return _poolDroneObjects.Dequeue();
            
                    PoolObject newDroneObject = Instantiate(dronePrefab);
                    _poolDroneObjects.Enqueue(newDroneObject);

                    return _poolDroneObjects.Dequeue();
            }

            return null;
        }
        
        public void ReturnToPool(PoolObject poolObject)
        {
            switch (poolObject.StageType)
            {
                case StageType.NormalCollectable:
                    _poolNormalCollectableObjects.Enqueue(poolObject);
                    break;
                case StageType.BigMultiplierCollectable:
                    _poolBigCollectableObjects.Enqueue(poolObject);
                    break;
                case StageType.Drone:
                    _poolDroneObjects.Enqueue(poolObject);
                    break;
            }
        }
    }
}
