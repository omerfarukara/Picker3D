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

        private Rigidbody _rigidbody;
        private PlayerMovement _playerMovement;

        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private bool _canMove;
        private bool _canThrowCollectables;

        public void ResetPosition()
        {
            transform.localPosition = _startPosition;
            transform.localRotation = _startRotation;
        }

        protected override void Awake()
        {
            base.Awake();

            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;

            _rigidbody = GetComponentInChildren<Rigidbody>();
            _playerMovement = new PlayerMovement(_rigidbody, forwardSpeed, horizontalSpeed);
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
    }
}