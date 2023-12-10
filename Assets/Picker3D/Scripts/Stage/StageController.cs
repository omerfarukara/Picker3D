using System;
using Picker3D.General;
using Picker3D.LevelSystem;
using Picker3D.Managers;
using Picker3D.PoolSystem;
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

        private void OnEnable()
        {
            GameManager.OnPitControl += CalculatePit;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.Collectable))
            {
                _collectedObjectCount++;
            }
        }

        private void OnDisable()
        {
            GameManager.OnPitControl -= CalculatePit;
        }

        public void Initialize()
        {
            _collectedObjectCount = 0;
            GameManager.OnCompleteStage += OnCompleteStageHandler;
            pitController.MoveDown();
            doorController.CloseDoor();
        }

        private void OnCompleteStageHandler()
        {
            GameManager.OnStageThrowControl?.Invoke();
        }

        private void CalculatePit()
        {
            if (_collectedObjectCount >= RequiredCollectableCount)
            {
                pitController.MoveUp();
            }
            else
            {
                GameManager.OnFailedStage?.Invoke();
            }

            GameManager.OnCompleteStage -= OnCompleteStageHandler;
        }
    }
}