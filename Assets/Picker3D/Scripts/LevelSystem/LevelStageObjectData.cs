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
        [SerializeField] private int requiredCollectableCount;
        [SerializeField] private Vector3[] positions;

        public StageType StageType
        {
            get => stageType;
            set => stageType = value;
        }
        public CollectableType CollectableType
        {
            get => collectableType;
            set => collectableType = value;
        }

        public int CollectableCount
        {
            get => collectableCount;
            set => collectableCount = value;
        }

        public int RequiredCollectableCount
        {
            get => requiredCollectableCount;
            set => requiredCollectableCount = value;
        }
        public Vector3[] Positions
        {
            get => positions;
            set => positions = value;
        }
    }
}