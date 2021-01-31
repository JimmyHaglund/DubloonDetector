using UnityEngine;
using UnityEngine.InputSystem;

namespace JimmyHaglund {
    /// <summary>
    /// Allows retreiving mouse hover info relatively effeciently by cachine hover info
    /// data until end of frame. 
    /// </summary>
    /// Should be placed far back in script execution heirarchy
    /// in order to guarantee that cached hover info data is cleared 
    /// before the next frame starts.
    public class MouseInfo : MonoSingleton<MouseInfo> {
        private static RaycastHit[] _currentMouseInfo = null;
        private static Camera _mainCamera = null;
        public static Camera MainCamera {
            get {
                if (_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
            set {
                _mainCamera = value;
            }
        }

        private void LateUpdate() {
            _currentMouseInfo = null;
        }

        public static RaycastHit[] GetMouseInfo(int layerMask = -1, Camera camera = null) {
            if (camera == null) camera = MainCamera;
            if (_currentMouseInfo != null) return _currentMouseInfo;
            Vector3 mousePos = GetMouseCameraNearPosition();
            Vector3 mouseFarPos = GetMouseCameraFarPosition();

            RaycastHit[] hitInfo = new RaycastHit[1];
            Physics.Raycast(mousePos, mouseFarPos - mousePos, out hitInfo[0], 300.0f, layerMask);
            return hitInfo;
        }

        public static Vector3 GetMouseCameraNearPosition() {
            var mousePos = MouseScreenPosition;
            return MainCamera.ScreenToWorldPoint(new Vector3(
                mousePos.x, mousePos.y, MainCamera.nearClipPlane));
        }

        public static Vector3 GetMouseCameraFarPosition() {
            var mousePos = MouseScreenPosition;
            return MainCamera.ScreenToWorldPoint(new Vector3(
                mousePos.x, mousePos.y, MainCamera.farClipPlane));
        }

        public static RaycastHit[] MouseHoverInfo(Camera camera) {
            return GetMouseInfo(-1, camera);
        }

        public static Vector2 MouseScreenPosition {
            get {
                var mouseControlPos = Mouse.current.position;
                var mouseX = mouseControlPos.x.ReadValue();
                var mouseY = mouseControlPos.y.ReadValue();
                return new Vector2(mouseX, mouseY);
            }
        }
    }
}