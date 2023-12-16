using System;
using System.Collections.Generic;
using Picker3D.General;
using Picker3D.LevelEditor;
using Picker3D.LevelSystem;
using UnityEngine;

namespace Picker3D.LevelSystem
{
    [Serializable]
    public class LevelStageObjectData
    {
        [SerializeField] private StageType stageType;
        [SerializeField] private CollectableType[] collectableTypes;
        [SerializeField] private Vector3[] positions;

        /// <summary>
        /// Level Stage Type : Normal, Big, Drone
        /// </summary>
        public StageType StageType
        {
            get => stageType;
            set => stageType = value;
        }

        /// <summary>
        /// Scene positions of collectable objects
        /// </summary>
        public Vector3[] Positions => positions;

        /// <summary>
        /// Collectable types of collectable objects
        /// </summary>
        public CollectableType[] CollectableTypes => collectableTypes;
        
        /// <summary>
        /// Count of collectable objects
        /// </summary>
        /// <returns> This method returned to count of collectable objects </returns>
        public int CollectableCount()
        {
            return collectableTypes.Length;
        }
        
        /// <summary>
        /// Required count of collectable objects
        /// </summary>
        /// <returns> Returns the number of objects required to pass the level. </returns>
        public int RequiredCollectableCount()
        {
            return CollectableCount() * 80 / 100;
        }

        /// <summary>
        /// It performs the recording of data.
        /// </summary>
        /// <param name="stageData"> Required class for data </param>
        public void SetData(StageData stageData)
        {
            stageType = stageData.StageType + 1;

            CollectableType[,] nodeData = stageData.CollectableNodeData;

            if (stageType is StageType.NormalCollectable or StageType.BigMultiplierCollectable)
            {
                SetCollectables(nodeData);
            }
        }

        public StageData GetStageData(int stageIndex)
        {
            StageData newStageData = new StageData
            {
                StageType = stageType,
                StageIndex = stageIndex,
                CollectableNodeData = GetCollectables()
            };

            return newStageData;
        }
        
        
        /// <summary>
        /// It processes the data and updates the appropriate variables.
        /// </summary>
        /// <param name="nodeData"></param>
        private void SetCollectables(CollectableType[,] nodeData)
        {
            int rowCount = nodeData.GetLength(1);
            int columnCount = nodeData.GetLength(0);
            
            List<Vector3> newPositions = new List<Vector3>();
            List<CollectableType> newCollectableValues = new List<CollectableType>();
            
            for (int column = 0; column < columnCount; column++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    if (nodeData[column, row] == CollectableType.None) continue;

                    Vector3 newPosition = new Vector3(column, 0.5f, row);
                    newPositions.Add(newPosition);
                    newCollectableValues.Add(nodeData[column, row]);
                }
            }

            positions = newPositions.ToArray();
            collectableTypes = newCollectableValues.ToArray();
        }

        /// <summary>
        /// It processes the data and updates the appropriate variables.
        /// </summary>
        /// <param name="nodeData"></param>
        private CollectableType[,] GetCollectables()
        {
            int columnCount = 0;
            int rowCount = 0;
            
            switch (stageType)
            {
                case StageType.NormalCollectable:
                    columnCount = GameConstants.NormalColumnCount;
                    rowCount = GameConstants.NormalRowCount;
                    break;
                case StageType.BigMultiplierCollectable:
                    columnCount = GameConstants.BigColumnCount;
                    rowCount = GameConstants.BigRowCount;
                    break;
            }

            CollectableType[,] nodeData = new CollectableType[columnCount, rowCount];

            for (int i = 0; i < positions.Length; i++)
            {
                nodeData[(int)positions[i].x, (int)positions[i].z] = collectableTypes[i];
            }
            
            return nodeData;
        }
    }
}