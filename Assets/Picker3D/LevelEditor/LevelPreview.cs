using System;
using Picker3D.LevelSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Picker3D.LevelEditor
{
    public class LevelPreview : MonoBehaviour
    {
        [SerializeField] private bool isPreview;

        private StageData[] _stagesData;
        
        private Vector3 _groundSize;
        private Vector3 _groundPosition;

        public void Preview(StageData[] stagesData)
        {
            _stagesData = stagesData;
            _groundPosition = Vector3.zero;
            _groundSize = new Vector3(11, 1, 55);
            isPreview = true;
        }

        private void OnDrawGizmos()
        {
            if (!isPreview || _stagesData == null) return;

            for (int i = 0; i < _stagesData.Length; i++)
            {
                Gizmos.color = new Color(0.31f, 0.72f, 1f);
                _groundPosition = Vector3.forward * i * 55f;
                Gizmos.DrawCube(_groundPosition , _groundSize);
                _groundPosition -= Vector3.forward * 22.5f;
                Gizmos.color = new Color(1f, 0.95f, 0f);
                StageType stageType = _stagesData[i].StageType + 1;

                CollectableType[,] nodeData = _stagesData[i].NormalCollectableNodeData;
                        
                int rowCount = nodeData.GetLength(1);
                int columnCount = nodeData.GetLength(0);
                
                switch (stageType)
                {
                    case StageType.NormalCollectable:
                        for (int column = 0; column < columnCount; column++)
                        {
                            for (int row = 0; row < rowCount; row++)
                            {
                                if (nodeData[column, row] == CollectableType.None) continue;
                                
                                Vector3 newPosition = new Vector3(row, 0.5f, column) / 2 + _groundPosition + Vector3.left * 5f;

                                Gizmos.DrawSphere(newPosition, 0.5f);
                            }
                        }
                        break;
                    case StageType.BigMultiplierCollectable:
                        
                        for (int column = 0; column < columnCount; column++)
                        {
                            for (int row = 0; row < rowCount; row++)
                            {
                                if (nodeData[column, row] == CollectableType.None) continue;
                                
                                Vector3 newPosition = new Vector3(row * 3 , 5f, column * 3) + _groundPosition + Vector3.left * 3f;

                                Gizmos.DrawSphere(newPosition, 3f);
                            }
                        }
                        break;
                    case StageType.Drone:
                        break;
                }
            }
        }
    }
}
