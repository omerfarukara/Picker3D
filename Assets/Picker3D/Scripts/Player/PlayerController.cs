using System;
using System.Collections;
using Picker3D.General;
using Picker3D.Managers;
using Picker3D.PoolSystem;
using Picker3D.Scripts.Helpers;
using Picker3D.StageObjects;
using Picker3D.StageObjets;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerController : MonoSingleton<PlayerController>
    {
        [SerializeField] private float forwardSpeed, horizontalSpeed;
        [SerializeField] private float horizontalBoundary;

        private Rigidbody _rigidbody;
        private PlayerMovement _playerMovement;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private bool _canMove;
        private bool _canThrowCollectables;

        protected override void Awake()
        {
            base.Awake();

            StartPositionAndRotationSet();

            _rigidbody = GetComponentInChildren<Rigidbody>();
            _playerMovement = new PlayerMovement(transform,_rigidbody, forwardSpeed, horizontalSpeed,horizontalBoundary);
        }

        private void OnEnable()
        {
            UIManager.OnStartButtonClicked += CanMoveTrue;
            GameManager.OnPassedStage += CanMoveTrue;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstants.StageControlPoint) &&
                other.transform.parent.TryGetComponent(out StageController stageController))
            {
                CanMoveFalse();
                CanThrowCollectableTrue(stageController);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_canThrowCollectables &&
                other.transform.parent.TryGetComponent(out NormalCollectable normalCollectableObj) &&
                !normalCollectableObj.IsThrow)
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
        private void CanThrowCollectableTrue(StageController stageController)
        {
            _canThrowCollectables = true;
            StartCoroutine(TriggerPitCalculate(stageController));
        }
        /// <summary>
        /// The ball launch is released and the current pit calculation is made.
        /// </summary>
        private IEnumerator TriggerPitCalculate(StageController stageController)
        {
            yield return new WaitForSeconds(1f);
            CanThrowCollectableFalse();
            stageController.CalculatePit();
        }
        private void CanThrowCollectableFalse()
        {
            _canThrowCollectables = false;
        }
        /// <summary>
        /// The player returns to the initial position and rotation for the game.
        /// </summary>
        public void ResetPosition()
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
        }
        /// <summary>
        /// The player saves the value for returning to the initial position and rotation for the game.
        /// </summary>
        private void StartPositionAndRotationSet()
        {
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
        }
    }
}