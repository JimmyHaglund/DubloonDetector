using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using InputInfoAction = System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>;

namespace JimmyHaglund.Input {

    public interface IInputHandler {
        IInputActionCollection InputSource { get; set; }
        Type InputSourceType { get; }
    }

    public interface IInputHandler<InputType> : IInputHandler where InputType : IInputActionCollection {
        new InputType InputSource { get; set; }
    }

    /// <summary>
    /// Base class for input receivers. Acts as a filter between the input module and the actual project.
    /// </summary>
    /// By using this we can separate game logic from whatever input system we're using, so that if we decide to
    /// switch input system, the InputHandlers will have to change internally but everything else will talk to them
    /// in the same way as always.
    public class InputHandler<InputType> : IInputHandler<InputType>, IInputHandler where InputType : IInputActionCollection {
        private struct InfoActionPair {
            public InputAction Input;
            public InputInfoAction Result;

            public InfoActionPair(InputAction input, InputInfoAction result) {
                Input = input;
                Result = result;
            }
        }

        private List<InfoActionPair> _storedActionsPerformed = new List<InfoActionPair>();
        protected InputType _inputSource;

        public Type InputSourceType {
            get {
                return typeof(InputType);
            }
        }

        public virtual InputType InputSource {
            get => _inputSource;
            set {
                if (_inputSource != null) ClearAllActions();
                _inputSource = value;
            }
        }

        IInputActionCollection IInputHandler.InputSource {
            get => _inputSource;
            set {
                if (value is InputType) {
                    InputSource = (InputType)value;
                }
            }
        }

        // public virtual void SetInputSource(InputType source) {

        // }
        /// <summary>
        /// Causes class to clear any event subscriptions to input system.
        /// </summary>
        private void ClearAllActions() {
            for (int n = 0; n < _storedActionsPerformed.Count; n++) {
                InfoActionPair pair = _storedActionsPerformed[n];
                pair.Input.performed -= pair.Result;
            }
            _storedActionsPerformed.Clear();
        }

        /// <summary>
        /// Adds resulting action to input pressed event.
        /// </summary>
        /// <param name="inputAction"></param>
        /// <param name="newAction"></param>
        protected void AddActionPerformed(InputAction inputAction, InputInfoAction newAction) {
            inputAction.performed += newAction;
            _storedActionsPerformed.Add(new InfoActionPair(inputAction, newAction));
        }
    }
}