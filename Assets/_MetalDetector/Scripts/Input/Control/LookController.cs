using UnityEngine;
using JimmyHaglund.Input;

namespace MetalDetector.Input.Control {
    public class LookController : MonoBehaviour {
        [SerializeField] private Float2Event _inputEvent = null;

        private FirstPersonInputHandler _inputHandler;

        private void Awake() {
            _inputHandler = InputManager.GetInput<FirstPersonInputHandler>();
        }

        private void Update() {
            if (_inputEvent == null) return;
            var input = _inputHandler.PollLook();
            _inputEvent.Raise(input.x, input.y);
        }
    }
}