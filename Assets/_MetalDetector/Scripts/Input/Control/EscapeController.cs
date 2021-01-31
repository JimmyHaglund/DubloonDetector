using UnityEngine;
using JimmyHaglund.Rapid;
using JimmyHaglund.Input;

namespace MetalDetector.Input.Control {
    public class EscapeController : MonoBehaviour {
        [SerializeField] private BoolEvent _pauseToggleEvent = null;
        [SerializeField] private BoolVariable _menuOpen = null;

        private void OnEnable() {
            var inputHandler = InputManager.GetInput<FirstPersonInputHandler>();
            inputHandler.Escape += ToggleMenu;
        }

        private void OnDisable() {
            var inputHandler = InputManager.GetInput<FirstPersonInputHandler>();
            inputHandler.Escape -= ToggleMenu;
        }

        private void ToggleMenu() {
            _menuOpen.Toggle();
            _pauseToggleEvent.Raise(_menuOpen.Value);
        }
    }
}