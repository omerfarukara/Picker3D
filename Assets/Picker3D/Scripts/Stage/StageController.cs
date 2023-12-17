using System;
using System.Collections.Generic;
using Picker3D.General;
using Picker3D.LevelSystem;
using Picker3D.Managers;
using Picker3D.PoolSystem;
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

        private readonly List<BaseCollectableObject> _collectableObjects = new List<BaseCollectableObject>();
        private bool _calculateCompleted;

        private void OnEnable()
        {
            GameManager.OnPitControl += CalculatePit;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstants.Collectable))
            {
                if (other.transform.parent.TryGetComponent(out BaseCollectableObject baseCollectableObject))
                {
                    _collectableObjects.Add(baseCollectableObject);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform.parent.TryGetComponent(out BaseCollectableObject baseCollectableObject))
            {
                _collectableObjects.Remove(baseCollectableObject);
            }
        }

        private void Update()
        {
            if (_calculateCompleted) return;
            pitText.text = $"{_collectableObjects.Count}/{RequiredCollectableCount}";
        }

        private void OnDisable()
        {
            GameManager.OnPitControl -= CalculatePit;
        }

        public void Initialize()
        {
            _collectedObjectCount = 0;
            _collectableObjects.Clear();
            _calculateCompleted = false;
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
            if (_collectableObjects.Count >= RequiredCollectableCount)
            {
                CollectablesProcess();
            }
            else
            {
                GameManager.OnCompleteStage -= OnCompleteStageHandler;
            }
        }

        private void CollectablesProcess()
        {
            foreach (BaseCollectableObject baseCollectable in _collectableObjects)
            {
                NormalCollectable obj = baseCollectable.GetComponent<NormalCollectable>();
                obj.ColorChange(stageMeshRenderer.sharedMaterial);
                obj.Explode();
            }

            _calculateCompleted = true;

            Invoke(nameof(PitMoveUp), 1);
        }

        private void PitMoveUp()
        {
            pitController.MoveUp();
        }
    }
}