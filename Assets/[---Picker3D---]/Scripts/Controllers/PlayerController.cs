using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float forwardSpeed, horizontalSpeed;

        private PlayerMovement _movement;

        private void Start()
        {
            _movement = new PlayerMovement(rb, forwardSpeed, horizontalSpeed);
        }

        private void FixedUpdate()
        {
            _movement.Move();
        }
    }
}