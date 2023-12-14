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
            stageType = stageData.StageType;

            CollectableType[,] nodeData = stageData.CollectableNodeData;
            
            switch (stageType)
            {
                case StageType.None:
                    break;
                case StageType.NormalCollectable:
                    SetNormalCollectables(nodeData);
                    break;
                case StageType.BigMultiplierCollectable:
                    SetBigCollectables(nodeData);
                    break;
                case StageType.Drone:
                    stageType = stageData.StageType;
                    break;
            }
        }

        public StageData GetStageData(int stageIndex)
        {
            StageData newStageData = new StageData
            {
                StageType = stageType,
                StageIndex = stageIndex
            };

            switch (stageType)
            {
                case StageType.None:
                    break;
                case StageType.NormalCollectable:
                    newStageData.CollectableNodeData = new CollectableType[GameConstants.NormalColumnCount, GameConstants.NormalRowCount];
                    break;
                case StageType.BigMultiplierCollectable:
                    newStageData.CollectableNodeData = new CollectableType[GameConstants.BigColumnCount, GameConstants.BigRowCount];
                    break;
                case StageType.Drone:
                    break;
            }
            
            return newStageData;
        }
        
        /// <summary>
        /// It processes the data and updates the appropriate variables.
        /// </summary>
        /// <param name="nodeData"></param>
        private void SetNormalCollectables(CollectableType[,] nodeData)
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
        private void SetBigCollectables(CollectableType[,] nodeData)
        {
            int rowCount = nodeData.GetLength(1);
            int columnCount = nodeData.GetLength(0);
            
            List<Vector3> newPositions = new List<Vector3>();
            
            for (int column = 0; column < columnCount; column++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    if (nodeData[column, row] == CollectableType.None) continue;

                    Vector3 newPosition = new Vector3(column, 5f, row);
                    newPositions.Add(newPosition);
                }
            }

            positions = newPositions.ToArray();
        }
    }
}