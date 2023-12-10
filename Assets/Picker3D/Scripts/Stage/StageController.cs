using System;
using Picker3D.General;
using Picker3D.LevelSystem;
using Picker3D.Managers;
using Picker3D.Stage;
using UnityEngine;

namespace Picker3D.StageObjets
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private PitController pitController;
        [SerializeField] private DoorController doorController;
        
        private int _collectedObjectCount;
        public int RequiredCollectableCount { get; set; }
        
        public void Initialize()
        {
            _collectedObjectCount = 0;
            GameManager.OnCompleteStage += OnCompleteStageHandler;
            pitController.MoveDown();
            doorController.CloseDoor();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Collectable))
            {
                _collectedObjectCount++;
            }
        }

        private void OnCompleteStageHandler()
        {
            if (_collectedObjectCount >= RequiredCollectableCount)
            {
                pitController.MoveUp();
                doorController.OpenDoor();
                GameManager.OnPassedStage?.Invoke();
            }
            else
            {
                GameManager.OnFailedStage?.Invoke();
            }
            
            GameManager.OnCompleteStage -= OnCompleteStageHandler;
        }
    }
}
