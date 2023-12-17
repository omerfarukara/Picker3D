using System;
using System.Collections.Generic;
using Picker3D.General;
using Picker3D.Managers;
using Picker3D.Scripts.StageObjets;
using Picker3D.Stage;
using Picker3D.StageObjects;
using TMPro;
using UnityEngine;

namespace Picker3D.StageObjets
{
    public class StageController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer stageMeshRenderer;
        [SerializeField] private TextMeshProUGUI pitText;
        [SerializeField] private PitController pitController;
        [SerializeField] private DoorController doorController;

        private int _collectedObjectCount;
        public int RequiredCollectableCount { get; set; }

        private readonly List<VisualStageObject> _collectableObjects = new List<VisualStageObject>();
        private bool _calculateCompleted;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstants.Collectable))
            {
                if (other.TryGetComponent(out VisualStageObject visualStageObject))
                {
                    _collectableObjects.Add(visualStageObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out VisualStageObject visualStageObject))
            {
                _collectableObjects.Remove(visualStageObject);
            }
        }

        private void Update()
        {
            if (_calculateCompleted) return;
            pitText.text = $"{_collectableObjects.Count}/{RequiredCollectableCount}";
        }

        public void Initialize()
        {
            _collectedObjectCount = 0;
            _collectableObjects.Clear();
            _calculateCompleted = false;
            pitController.MoveDown();
            doorController.CloseDoor();
        }

        public void CalculatePit()
        {
            if (_collectableObjects.Count >= RequiredCollectableCount)
            {
                CollectablesProcess();
            }
            else
            {
                GameManager.OnFailedStage?.Invoke();
            }
        }

        private void CollectablesProcess()
        {
            foreach (VisualStageObject visualStageObject in _collectableObjects)
            {
                visualStageObject.ColorChange(true);
                visualStageObject.Explode();
            }

            Invoke(nameof(Passed), 1);
        }

        private void Passed()
        {
            _calculateCompleted = true;
            pitController.MoveUp();
        }

        public void ResetStage()
        {
            pitController.MoveDown();
            doorController.CloseDoor();
        }
    }
}