using UnityEngine;

namespace Assets._MetalDetector.Scripts {
    public class CharacterLook : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _bodyTransform;
        [Header("Settings")]
        [SerializeField] private float _sensitivityX = 1.0f;
        [SerializeField] private float _sensitivityY = 1.0f;
        [SerializeField] private bool _invertX = false;
        [SerializeField] private bool _invertY = false;

        private Vector2 _input = default;

        private float XMultiplier => (_invertX ? -1 : 1) * _sensitivityX;
        private float YMultiplier => (_invertY ? -1 : 1) * _sensitivityY;

        public void SetInput(float x, float y) {
            _input = new Vector2(x, y);
        }

        private void Update() {
            var bodyRotation = Quaternion.AngleAxis(_input.x * XMultiplier, Vector3.up);
            _bodyTransform.rotation *= bodyRotation;
            var headRotation = Quaternion.AngleAxis(_input.y * YMultiplier, Vector3.right);
            _headTransform.localRotation *= headRotation;
            var headAngle = _headTransform.eulerAngles.x;
            if (!HeadFacingForwardRelativeToBody()) {
                var eulerY = _headTransform.rotation.eulerAngles.y;
                var eulerZ = _headTransform.rotation.eulerAngles.z;
                if (HeadFacingUp()) {
                    _headTransform.rotation = Quaternion.Euler(-90, eulerY, eulerZ);
                } else {
                    _headTransform.rotation = Quaternion.Euler(90, eulerY, eulerZ);
                }
            }
        }

        private bool HeadFacingForwardRelativeToBody() {
            var angle = Vector3.Angle(_bodyTransform.forward, _headTransform.forward);
            return angle <= 90;
        }

        private bool HeadFacingUp() {
            return Vector3.Angle(_bodyTransform.up, _headTransform.forward) < 90;
        }
    }
}