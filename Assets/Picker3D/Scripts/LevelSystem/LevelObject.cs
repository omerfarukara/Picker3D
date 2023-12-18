using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class LevelObject : MonoBehaviour
    {
        [SerializeField] private LevelStageObject levelStageObjectPrefab;

        private readonly Queue<LevelStageObject> _levelStageObjects = new Queue<LevelStageObject>();
        
        /// <summary>
        /// Build new level objects.
        /// </summary>
        public void Build(LevelObjectData levelObjectData, int currentPlayedLevelCount)
        {
            transform.position = Vector3.forward * currentPlayedLevelCount;

            for (int i = 0; i < levelObjectData.LevelStageObjectsData.Length; i++)
            {
                LevelStageObjectData levelStageObjectData = levelObjectData.LevelStageObjectsData[i];
                GetNewLevelStageObject().Build(levelStageObjectData, i, ReturnToQueue);
            }
        }
        
        /// <summary>
        /// It retrieves the current level's stage information.
        /// </summary>
        private LevelStageObject GetNewLevelStageObject()
        {
            if (_levelStageObjects.Count != 0) return _levelStageObjects.Dequeue();

            LevelStageObject newLevelStageObject = Instantiate(levelStageObjectPrefab);
            _levelStageObjects.Enqueue(newLevelStageObject);

            return _levelStageObjects.Dequeue();
        }

        /// <summary>
        /// Return to queue level stage objects
        /// </summary>
        private void ReturnToQueue(LevelStageObject levelStageObject)
        {
            _levelStageObjects.Enqueue(levelStageObject);
        }
    }
}