using JimmyHaglund;
using UnityEngine;
using UnityEngine.Events;

namespace MetalDetector {
    public class MetalDetectorLightBlinker : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private Light _blinkLight = null;
        [SerializeField] private Renderer _blinkRenderer = null;
        [Header("Settings")]
        [SerializeField] private Color _blinkLightOnColor = Color.green;
        [SerializeField] private Color _blinkLightOffColor = Color.black;
        [SerializeField] private AnimationCurve _blinkIntervalSecondsPerMetreFromMetal;
        [SerializeField] [Min(0.0f)] private float _minDistanceTreshold = 1.0f;
        [SerializeField] [Min(0.0f)] private float _maxDistanceTreshold = 10.0f;
        [SerializeField] [Min(0.1f)] private float _minFrequencyTreshold = 0.5f;
        [SerializeField] [Min(0.2f)] private float _maxFrequencyTreshold = 5.0f;
        [Header("Events")]
        [SerializeField] private UnityEvent _onLightEnabled;

        private float _blinkFrequency = 1.0f;
        private float _timeSinceLastBlink = 0.0f;
        private bool _blinkOn = false;

        private Color BlinkColor => _blinkOn ? _blinkLightOnColor : _blinkLightOffColor;
        private float BlinkThreshold => _blinkOn ? 0.2f : _blinkFrequency;

        public void SetDistanceToMetal(float distance) {
            var clampedDistance = Mathf.Clamp(distance, 0.0f , _maxDistanceTreshold);
            if (distance < _minDistanceTreshold) clampedDistance = 0.0f;
            var relativeDistance = clampedDistance / (_maxDistanceTreshold);
            var relativeFrequency = _blinkIntervalSecondsPerMetreFromMetal.Evaluate(relativeDistance);
            var totalFrequency = relativeFrequency * (_maxFrequencyTreshold);
            _blinkFrequency = Mathf.Clamp(totalFrequency, _minFrequencyTreshold, _maxFrequencyTreshold);
        }


        private void Update() {
            _timeSinceLastBlink += Time.deltaTime;
            if (_timeSinceLastBlink > BlinkThreshold) {
                ToggleBlink();
                _timeSinceLastBlink = 0.0f;
            }
        }

        private void ToggleBlink() {
            _blinkOn = !_blinkOn;
            if (_blinkOn) _onLightEnabled.Invoke();
            if (_blinkLight == null || _blinkRenderer == null) return;
            _blinkLight.enabled = _blinkOn;
            _blinkRenderer.material.SetColor("_EmissionColor", BlinkColor);
        }



    }
}
