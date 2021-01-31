using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using JimmyHaglund;

namespace MetalDetector {
    public class FirstPersonAnimatorPropertySetter : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] [ChildReferenceButton] private Animator _animator;
        [Header("Settings")]
        [SerializeField] private string _movementMagnitudePropertyName = "speed";
        [SerializeField] private string _digTriggerName = "dig";
        [SerializeField] private UnityEvent _onShovelHitDirt;

        public void SetMovementMagnitude(float magnitude) {
            _animator.SetFloat("speed", magnitude);
        }

        public void SetDigStart() {
            _animator.SetTrigger("dig");
        }

        public void InvokeDug() {
            _onShovelHitDirt.Invoke();
        }
    }
}