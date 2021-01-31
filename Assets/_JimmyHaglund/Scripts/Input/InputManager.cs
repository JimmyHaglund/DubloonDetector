using UnityEngine.InputSystem;
using System.Collections.Generic;
using System;

namespace JimmyHaglund.Input {
    /// <summary>
    /// Input manager that handles all user input and filters input based on game state.
    /// </summary>
    /// Singleton class.
    public class InputManager {
        protected InputManager() {
            _instance = _instance ?? this;
        }

        protected Dictionary<Type, IInputHandler> _inputHandlers
            = new Dictionary<Type, IInputHandler>();

        private Dictionary<Type, IInputActionCollection> _inputSettings
            = new Dictionary<Type, IInputActionCollection>();

        // private InputHandler<InputType> _activeInput = null;
        // private InputType _inputSystem;
        private static InputManager _instance;
        protected static InputManager Instance {
            get {
                if (_instance == null) {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Return input of target type.
        /// </summary>
        /// <typeparam name="InputHandlerType"></typeparam>
        /// <returns>Input of desired type.</returns>
        /// Guaranteed to return an input. If the desired input has not been
        /// instantiated, it will first be instantiated and then returned.
        public static InputHandlerType GetInput<InputHandlerType>() where InputHandlerType : class, IInputHandler, new() {
            IInputHandler returnValue;
            if (!Instance._inputHandlers.TryGetValue(typeof(InputHandlerType), out returnValue)) {
                returnValue = new InputHandlerType();
                Instance._inputHandlers.Add(typeof(InputHandlerType), returnValue);
                IInputActionCollection source = GetControlSettings(returnValue.InputSourceType);
                returnValue.InputSource = source;
            }
            return returnValue as InputHandlerType;
        }

        /// <summary>
        /// Retreives input source, creates one if none exists. Enables the newly created input source.
        /// </summary>
        /// <param name="settingsType"></param>
        /// <returns></returns>
        private static IInputActionCollection GetControlSettings(Type settingsType) {
            if (_instance._inputSettings.ContainsKey(settingsType)) {
                return _instance._inputSettings[settingsType];
            }
            IInputActionCollection newSettings = Activator.CreateInstance(settingsType) as IInputActionCollection;
            _instance._inputSettings[settingsType] = newSettings;
            newSettings.Enable();
            return newSettings;
        }
    }
}