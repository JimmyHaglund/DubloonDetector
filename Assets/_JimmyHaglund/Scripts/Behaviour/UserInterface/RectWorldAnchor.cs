using UnityEngine;

namespace JimmyHaglund.Behaviours.UserInterface {
    [RequireComponent(typeof(RectTransform))]
    public class RectWorldAnchor : MonoBehaviour {
        [SerializeField] private Camera _camera = null;
        [SerializeField] private Canvas _parentCanvas = null;
        [SerializeField] private Vector3 _worldPosition = Vector3.zero;
        private RectTransform _rectTransform;

        public Vector3 WorldPosition {
            get => _worldPosition;
            set {
                _worldPosition = value;
                enabled = _worldPosition != null;
            }
        }

        private void Awake() {
            _rectTransform = transform as RectTransform;
        }

        private void Start() {
            if (_worldPosition == null) {
                enabled = false;
                return;
            }
            if (_camera == null) _camera = Camera.main;
        }

        private void Update() {
            if (!enabled) {
                enabled = false;
                return;
            }
            SetCanvasPosition();
        }

        private void SetCanvasPosition() {
            var canvasPosition = _camera.WorldToScreenPoint(_worldPosition);
            canvasPosition /= _parentCanvas.scaleFactor;
            _rectTransform.anchoredPosition = canvasPosition;
        }
    }
}