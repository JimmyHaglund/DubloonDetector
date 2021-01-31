using UnityEngine;

namespace JimmyHaglund.Display {
    public class ScreenRectangleDisplayer : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private MouseTracker _mouseTracker = null;
        [Header("Settings")]
        [SerializeField] private Color _fillColor = Color.gray - new Color(0.0f, 0.0f, 0.0f, 0.75f);
        [SerializeField] private Color _borderColor = Color.black;
        [SerializeField] [Min(0.0f)] private float _borderThickness = 2.0f;

        private bool _isDisplaying = false;
        private Vector2 _rectangleStartScreenPosition = default;

        public void StartDisplay() {
            _isDisplaying = true;
            _rectangleStartScreenPosition = MouseInfo.MouseScreenPosition;
        }

        public void StopDisplay() {
            _isDisplaying = false;
        }

        private void OnGUI() {
            if (_isDisplaying && enabled) {
                Vector2 mouseScreenPosition = _mouseTracker != null ? _mouseTracker.MouseScreenPosition : default; 
                Rect selection = ScreenRect.GetScreenRect(_rectangleStartScreenPosition, mouseScreenPosition);
                ScreenRect.DrawScreenRect(selection, _fillColor);
                ScreenRect.DrawScreenRectBorders(selection, _borderColor, _borderThickness);
            }
        }
    }
}