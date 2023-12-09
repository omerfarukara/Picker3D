using System.Collections.Generic;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    public class LevelObject : MonoBehaviour
    {
        [SerializeField] private LevelStageObject levelStageObjectPrefab;
        
        private readonly Queue<LevelStageObject> _levelStageObjects = new Queue<LevelStageObject>();
        
        public void Build(LevelObjectData levelObjectData, int currentPlayedLevelCount)
        {
            transform.position = Vector3.forward * currentPlayedLevelCount;
            
            // Build the level object

            foreach (LevelStageObjectData levelStageObjectData in levelObjectData.LevelStageObjectsData)
            {
                GetNewLevelStageObject().Build(levelStageObjectData);
            }
        }

        private LevelStageObject GetNewLevelStageObject()
        {
            if (_levelStageObjects.Count != 0) return _levelStageObjects.Dequeue();
            
            LevelStageObject newLevelStageObject = Instantiate(levelStageObjectPrefab);
            _levelStageObjects.Enqueue(newLevelStageObject);

            return _levelStageObjects.Dequeue();
        }
    }
}