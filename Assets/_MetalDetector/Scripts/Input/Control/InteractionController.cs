using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JimmyHaglund.Rapid;
using JimmyHaglund.Input;

namespace MetalDetector.Input.Control {
    public class InteractionController : MonoBehaviour {
        [SerializeField] private GameEvent _useStart = null;
        [SerializeField] private GameEvent _useStop = null;

        private void OnEnable() {
            var inputHandler = InputManager.GetInput<FirstPersonInputHandler>();
            inputHandler.StartUse += InvokeStartUse;
            inputHandler.StopUse += InvokeStopUse; 
        }

        private void OnDisable() {
            var inputHandler = InputManager.GetInput<FirstPersonInputHandler>();
            inputHandler.StartUse -= InvokeStartUse;
            inputHandler.StopUse -= InvokeStopUse;
        }

        private void InvokeStartUse() {
            if (_useStart == null) return;
            _useStart.Raise();
        }

        private void InvokeStopUse() {
            if (_useStop == null) return;
            _useStop.Raise();
        }
    }
}