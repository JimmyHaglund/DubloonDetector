using UnityEngine;

namespace JimmyHaglund.Behaviours.Movement {
    public class BasicMovement : MonoBehaviour, IMoveable {
        [SerializeField] protected float _maxSpeedMetresPerSecond = 4.0f;
        private Vector3 _velocity;

        public float MaxSpeedMetresPerSecond { get => _maxSpeedMetresPerSecond; set => _maxSpeedMetresPerSecond = value; }

        public void Accellerate(float accellerationX, float accellerationY, float accellerationZ) {
            var accelleration = new Vector3(accellerationX, accellerationY, accellerationZ);
            _velocity = Vector3.ClampMagnitude(_velocity + accelleration, MaxSpeedMetresPerSecond);
        }

        public void Displace(float dX, float dY, float dz) {
            transform.position += new Vector3(dX, dY, dz);
        }

        private void Move(Vector3 velocity) {
            transform.position += _velocity * Time.smoothDeltaTime;
        }

        public void SetVelocity(float velocityX, float velocityY, float velocityZ) {
            var targetVelocity = new Vector3(velocityX, velocityY, velocityZ);
            _velocity = Vector3.ClampMagnitude(targetVelocity, MaxSpeedMetresPerSecond);
        }
    }
}