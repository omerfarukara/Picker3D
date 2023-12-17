using System;
using Picker3D.General;
using Picker3D.Managers;
using Picker3D.PoolSystem;
using Picker3D.Scripts.Helpers;
using Picker3D.StageObjects;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        [SerializeField] private float forwardSpeed, horizontalSpeed;

        private Rigidbody _rigidbody;
        private PlayerMovement _playerMovement;

        private bool _canMove;
        private bool _canThrowCollectables;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _playerMovement = new PlayerMovement(_rigidbody, forwardSpeed, horizontalSpeed);
        }

        private void OnEnable()
        {
            UIManager.OnStartButtonClicked += CanMoveTrue;
            GameManager.OnStageThrowControl += OnStageThrowControlHandler;
            GameManager.OnPassedStage += CanMoveTrue;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstants.StageControlPoint))
            {
                GameManager.OnCompleteStage?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_canThrowCollectables && other.transform.parent.TryGetComponent(out NormalCollectable normalCollectableObj) && !normalCollectableObj.IsThrow)
            {
                normalCollectableObj.Throw();
            }
        }
        private void FixedUpdate()
        {
            if (!_canMove) return;

            _playerMovement.Move();
        }

        private void OnDisable()
        {
            UIManager.OnStartButtonClicked -= CanMoveTrue;
            GameManager.OnStageThrowControl -= OnStageThrowControlHandler;
            GameManager.OnPassedStage -= CanMoveTrue;
        }

        private void CanMoveTrue()
        {
            _canMove = true;
        }
        private void CanMoveFalse()
        {
            _canMove = false;
        }
        private void CanThrowCollectableTrue()
        {
            _canThrowCollectables = true;
            Invoke(nameof(TriggerPitCalculate),1);
        }
        private void OnStageThrowControlHandler()
        {
            CanMoveFalse();
            CanThrowCollectableTrue();
        }

        private void TriggerPitCalculate()
        {
            CanThrowCollectableFalse();
            GameManager.OnPitControl?.Invoke();
        }
        
        private void CanThrowCollectableFalse()
        {
            _canThrowCollectables = false;
        }
    }
}