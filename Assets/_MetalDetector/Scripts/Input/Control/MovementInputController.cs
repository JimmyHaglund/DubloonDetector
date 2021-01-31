using UnityEngine;
using JimmyHaglund.Input;

namespace MetalDetector.Input.Control {
    public class MovementInputController : MonoBehaviour {
        private FirstPersonInputHandler _inputHandler;
        [SerializeField] private Float2Event _inputEvent = null;

        private void Awake() {
            _inputHandler = InputManager.GetInput<FirstPersonInputHandler>();
        }

        private void Update() {
            if (_inputEvent == null) return;
            var input = _inputHandler.PollMove();
            _inputEvent.Raise(input.x, input.y);
        }
    }
}