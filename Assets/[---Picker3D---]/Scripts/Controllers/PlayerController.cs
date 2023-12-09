using System;
using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed, horizontalSpeed;

        private Rigidbody _rigidbody;
        private PlayerMovement _movement;

        private void Awake()
        {
            _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        private void Start()
        {
            _movement = new PlayerMovement(_rigidbody, forwardSpeed, horizontalSpeed);
        }

        private void FixedUpdate()
        {
            _movement.Move();
        }
    }
}