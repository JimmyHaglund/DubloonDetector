using UnityEngine;
using UnityEngine.InputSystem;

namespace JimmyHaglund {
    [CreateAssetMenu(menuName = "JimmyHaglund/Input/Mouse Tracker")]
    public class MouseTracker : ScriptableObject {
        [SerializeField] private LayerMask _collisionLayers;
        private Camera _mainCamera = null;
        
        public Camera ViewCamera {
            get {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
            set {
                _mainCamera = value;
            }
        }

        public RaycastHit[] GetMouseInfo() {
            Vector3 mousePos = GetMouseCameraNearPosition();
            Vector3 mouseFarPos = GetMouseCameraFarPosition();

            RaycastHit[] hitInfo = new RaycastHit[1];
            Physics.Raycast(mousePos, mouseFarPos - mousePos, out hitInfo[0], 300.0f, _collisionLayers);
            return hitInfo;
        }

        public Vector3 GetMouseCollisionPoint() {
            return GetMouseInfo()[0].point;
        }

        public Vector3 GetMouseCameraNearPosition() {
            var mousePos = MouseScreenPosition;
            return ViewCamera.ScreenToWorldPoint(new Vector3(
                mousePos.x, mousePos.y, ViewCamera.nearClipPlane));
        }

        public Vector3 GetMouseCameraFarPosition() {
            var mousePos = MouseScreenPosition;
            return ViewCamera.ScreenToWorldPoint(new Vector3(
                mousePos.x, mousePos.y, ViewCamera.farClipPlane));
        }

        public Vector2 MouseScreenPosition {
            get {
                var mouseControlPos = Mouse.current.position;
                var mouseX = mouseControlPos.x.ReadValue();
                var mouseY = mouseControlPos.y.ReadValue();
                return new Vector2(mouseX, mouseY);
            }
        }
    }
}