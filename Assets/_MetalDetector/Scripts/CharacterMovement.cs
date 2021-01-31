using System;
using UnityEngine;
using UnityEngine.Events;
using JimmyHaglund;

namespace MetalDetector {

    public class CharacterMovement : MonoBehaviour {
        [Serializable] public class FloatUnityEvent : UnityEvent<float> { }
        [Header("Dependencies")]
        [SerializeField] [ChildReferenceButton] private Camera _camera = null;
        [SerializeField] [ChildReferenceButton] private CharacterController _characterController = null;
        [Header("Settings")]
        [SerializeField] private float _movementAccelleration = 3.0f;
        [SerializeField] private float _brakeDecelleration = 6.0f;
        [SerializeField] private float _maxMovementSpeed = 3.0f;
        [SerializeField] private float _gravity = 100.0f;
        [SerializeField] private float _maxFallSpeed = 6.0f;
        [SerializeField] LayerMask _collisionLayers = default;
        [Header("Events")]
        [SerializeField] private FloatUnityEvent _inputSquareMagnitudeSet;

        private Vector3 _velocity = default;
        private Vector3 _up = Vector3.up;
        private bool _braking = true;
        private Vector2 _input;

        private Vector3 HorizontalVelocity => new Vector3(_velocity.x, 0.0f, _velocity.z);
        private Vector3 VerticalVelocity => new Vector3(0.0f, _velocity.y, 0.0f);

        private void Update() {
            _up = FindGroundNormal();
            ApplyLocomotion();
            ApplyGravity();
            ApplyBraking();
            _velocity = GetClampedVelocity();
            Move();
            _braking = true;
        }

        public void AddInput(float moveX, float moveY) {
            _input = new Vector2(moveX, moveY);
            _braking = _input.sqrMagnitude < 0.01f;
            _inputSquareMagnitudeSet.Invoke(_input.sqrMagnitude);
        }

        private Vector3 FindGroundNormal() {
            if (RayCastGround(out var hitInfo)) {
                return hitInfo.normal;
            }
            return Vector3.up;
        }

        private void ApplyLocomotion() {
            var moveY = _input.y;
            var moveX = _input.x;
            var deltaVelocity = transform.forward * moveY + transform.right * moveX;
            deltaVelocity = TransformVectorFromGlobalUpNormal(deltaVelocity, _up);
            deltaVelocity *= _movementAccelleration * Time.smoothDeltaTime;
            // Debug.Log(deltaVelocity);
            _velocity += deltaVelocity;
        }

        private Vector3 TransformVectorFromGlobalUpNormal(Vector3 vector, Vector3 targetNormal) {
            var angle = Vector3.Angle(Vector3.up, targetNormal);
            var rotation = Quaternion.AngleAxis(angle, transform.right);
            return rotation * vector;
        }

        private bool RayCastGround(out RaycastHit hitInfo) {
            return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hitInfo, 1.5f, _collisionLayers.value, QueryTriggerInteraction.Ignore);
        }

        private void ApplyGravity() {
            if (_characterController.isGrounded) {
                _velocity.y = 0.0f;
                return;
            }
            _velocity += Vector3.down * _gravity * Time.smoothDeltaTime;
        }

        private Vector3 GetClampedVelocity() {
            return Vector3.ClampMagnitude(HorizontalVelocity, _maxMovementSpeed) +
                Vector3.ClampMagnitude(VerticalVelocity, _maxFallSpeed);
        }

        private void ApplyBraking() {
            if (!_braking) return;
            if (HorizontalVelocity.sqrMagnitude < 0.1f) _velocity = VerticalVelocity;
            _velocity -= HorizontalVelocity.normalized * _brakeDecelleration * Time.smoothDeltaTime;
        }

        private void Move() {
            _characterController.Move(_velocity * Time.smoothDeltaTime);
        }
    }

}