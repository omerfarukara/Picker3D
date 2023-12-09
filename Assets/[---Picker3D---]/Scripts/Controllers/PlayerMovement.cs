using Picker3D.UI;
using UnityEngine;

namespace Picker3D.Player
{
    public class PlayerMovement
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _horizontalSpeed;
        private readonly float _forwardSpeed;
    
        public PlayerMovement(Rigidbody rigidbody, float forwardSpeed, float horizontalSpeed)
        {
            _rigidbody = rigidbody;
            _forwardSpeed = forwardSpeed;
            _horizontalSpeed = horizontalSpeed;
        }

        public void Move()
        {
            Vector3 direction = new Vector3(_horizontalSpeed * UIInput.Instance.GetHorizontal(), 0, _forwardSpeed);
            _rigidbody.MovePosition(_rigidbody.position + direction * Time.fixedDeltaTime);
        }
    }
}