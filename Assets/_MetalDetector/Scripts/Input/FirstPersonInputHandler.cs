using System;
using JimmyHaglund.Input;
using UnityEngine;

namespace MetalDetector.Input {
    public class FirstPersonInputHandler : InputHandler<FirstPersonInputActions> {
        public event Action StartUse;
        public event Action StopUse;
        public event Action Escape;

        public override FirstPersonInputActions InputSource {
            set {
                _inputSource = value;
                AddActionPerformed(_inputSource.Interaction.Use, (c) => {
                    if (c.ReadValue<float>() > 0.5f) StartUse?.Invoke();
                    else StopUse?.Invoke();
                });
                AddActionPerformed(_inputSource.Escape.Pause, (c) => Escape?.Invoke());
            }
        }

        public Vector2 PollMove() {
            if (_inputSource == null) return default;
            return _inputSource.Move.Move.ReadValue<Vector2>();
        }

        public Vector2 PollLook() {
            if (_inputSource == null) return default;
            return _inputSource.Look.Look.ReadValue<Vector2>();
        }
    }
}