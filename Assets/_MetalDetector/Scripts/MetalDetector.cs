using System.Collections;
using UnityEngine;
using JimmyHaglund.Rapid;

namespace MetalDetector {
    public class MetalDetector : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] private TransformListSet _treasureSet;
        [SerializeField] private FloatEvent _distanceUpdatedEvent;
        [Header("Settings")]
        [SerializeField] [Min(0.1f)] private float _updateTimer;
        private WaitForSeconds _wait;
        private Vector3 _targetPosition = default;

        public float CalculateDistanceToTarget() {
            return Vector3.Distance(transform.position, _targetPosition);
        }

        private void Start() {
            _wait = new WaitForSeconds(_updateTimer);
            StartCoroutine(CO_UpdateTargetRepeating());
        }

        private IEnumerator CO_UpdateTargetRepeating() {
            while (true) {
                _targetPosition = FindTargetTreasure();
                RaiseDistanceUpdated();
                yield return _wait;
            }
        }

        private Vector3 FindTargetTreasure() {
            if (_treasureSet == null) return default;
            var target = default(Vector3);
            var targetSqrDistance = Vector3.SqrMagnitude(transform.position - target);
            foreach(Transform item in _treasureSet.Items) {
                var sqrDistance = Vector3.SqrMagnitude(transform.position - item.position);
                if (sqrDistance < targetSqrDistance) {
                    target = item.position;
                    targetSqrDistance = sqrDistance;
                }
            }
            return target;
        }

        private void RaiseDistanceUpdated() {
            if (_distanceUpdatedEvent == null) return;
            _distanceUpdatedEvent.Raise(CalculateDistanceToTarget());
        }
#if UNITY_EDITOR
        private void OnValidate() {
            if (Application.isPlaying) {
                _wait = new WaitForSeconds(_updateTimer);
            }
        }
#endif
    }
}