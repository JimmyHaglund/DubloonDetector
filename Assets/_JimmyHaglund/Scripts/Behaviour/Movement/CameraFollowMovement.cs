using UnityEngine;

namespace JimmyHaglund.Behaviours.Movement {
    public class CameraFollowMovement : MonoBehaviour, IMoveable {
        [SerializeField] private float _moveSpeedMetresPerSecond = 10.0f;
        [SerializeField] private Vector3 _rayCastOriginOffset = Vector3.up * 5.0f;
        [SerializeField] [Range(1.0f, 20.0f)] private float _altitudeLerpSpeed = 5.0f;
#if (UNITY_EDITOR)
        [Header("Gizmo settings")]
        [SerializeField] private bool _drawGizmos = false;
        [SerializeField] private Color _gizmoColor = Color.green - new Color(0.0f, 0.0f, 0.0f, 0.25f);
#endif

        public float MaxSpeedMetresPerSecond { get => _moveSpeedMetresPerSecond; set => _moveSpeedMetresPerSecond = value; }
        private Vector3 Position => transform.position;

        public void Displace(float dX, float dY, float dZ) {
            var velocity = new Vector3(dX, dY, dZ);
            Displace(velocity);
        }

        public void SetVelocity(float velocityX, float velocityY, float velocityZ) { }

        public void Accellerate(float accellerationX, float accellerationY, float accellerationZ) { }

        private void Displace(Vector3 velocity) {
            transform.position += GetDeltaMovement(velocity);
            if (CheckRaycastFromAboveDownwards(out RaycastHit hitInfo)) {
                var targetY = Mathf.Lerp(Position.y, hitInfo.point.y, _altitudeLerpSpeed * Time.deltaTime);
                transform.position = new Vector3(Position.x, targetY, Position.z);
            }
        }

        private bool CheckRaycastFromAboveDownwards(out RaycastHit hitInfo) {
            return Physics.Raycast(Position + _rayCastOriginOffset, Vector3.down, out hitInfo);
        }

        private Vector3 GetDeltaMovement(Vector3 velocity) {
            return velocity * _moveSpeedMetresPerSecond * Time.deltaTime;
        }

#if (UNITY_EDITOR)
        private void OnDrawGizmos() {
            if (!_drawGizmos) return;
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(Position + _rayCastOriginOffset, 0.25f);
            Gizmos.DrawLine(Position + _rayCastOriginOffset, Position);
            Gizmos.DrawSphere(Position, 0.25f);
        }
#endif
    }
}