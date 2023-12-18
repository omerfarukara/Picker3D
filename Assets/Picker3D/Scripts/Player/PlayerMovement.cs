using Picker3D.UI;
using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerMovement
    {
        private Vector3 _direction;
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly float _horizontalSpeed;
        private readonly float _forwardSpeed;
        private readonly float _horizontalBoundary;

        public PlayerMovement(Transform transform, Rigidbody rigidbody, float forwardSpeed, float horizontalSpeed,
            float horizontalBoundary)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _forwardSpeed = forwardSpeed;
            _horizontalSpeed = horizontalSpeed;
            _horizontalBoundary = horizontalBoundary;
        }

        /// <summary>
        /// Player move
        /// </summary>
        public void Move()
        {
            BoundaryControl();
            _rigidbody.MovePosition(_rigidbody.position + _direction * Time.fixedDeltaTime);
        }

        /// <summary>
        /// The player has the necessary control to not leave the platform.
        /// </summary>
        private void BoundaryControl()
        {
            bool leftControl = (_transform.position.x > _horizontalBoundary && UIInput.Instance.GetHorizontal() > 0);
            bool rightControl = (_transform.position.x < -_horizontalBoundary && UIInput.Instance.GetHorizontal() < 0);

            if (leftControl || rightControl)
            {
                _direction = new Vector3(0, 0, _forwardSpeed);
            }
            else
            {
                _direction = new Vector3(_horizontalSpeed * UIInput.Instance.GetHorizontal(), 0, _forwardSpeed);
            }
        }
    }
}