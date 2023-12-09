using System;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed, horizontalSpeed;

        private Rigidbody _rigidbody;
        private PlayerMovement _playerMovement;

        private bool _canMove;

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
            _playerMovement = new PlayerMovement(_rigidbody, forwardSpeed, horizontalSpeed);
        }

        private void OnEnable()
        {
            UIManager.OnStartButtonClicked += OnStartButtonClickedHandler;
        }

        private void FixedUpdate()
        {
            if(!_canMove) return;
            
            _playerMovement.Move();
        }
        
        private void OnStartButtonClickedHandler()
        {
            _canMove = true;
        }
    }
}