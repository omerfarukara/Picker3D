using System;
using Picker3D.LevelSystem;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    [Serializable]
    public class LevelStageObjectData
    {
        [SerializeField] private StageType stageType;
        [SerializeField] private CollectableType collectableType;
        [SerializeField] private int collectableCount;
        [SerializeField] private Vector3[] positions;
        
        public LevelStageObjectData(StageType stageType, CollectableType collectableType, int collectableCount, Vector3[] positions)
        {
            this.stageType = stageType;
            this.collectableType = collectableType;
            this.collectableCount = collectableCount;
            this.positions = positions;
        }
        
        public StageType StageType => stageType;
        public CollectableType CollectableType => collectableType;
        public int CollectableCount => collectableCount;
        public Vector3[] Positions => positions;
    }
}