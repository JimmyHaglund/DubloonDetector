// GENERATED AUTOMATICALLY FROM 'Assets/_MetalDetector/Input/FirstPersonInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MetalDetector.Input
{
    public class @FirstPersonInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @FirstPersonInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""FirstPersonInputActions"",
    ""maps"": [
        {
            ""name"": ""Move"",
            ""id"": ""1017ad11-411d-400f-90ab-98df58843ac2"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8f9bd189-0a2a-42fa-90b0-ef603fe1c81a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1a7fff9d-ae29-45a9-af95-2721774b0340"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7444fdc8-5b8c-4e5e-a0aa-f93ebef97d39"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d0bdbe1d-5473-4cc9-8b5a-f01f9a594b22"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""53b56404-d3a5-4136-bc7e-e0d25d47134a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1dd96ca1-53a1-4010-be00-6f7231754a04"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Look"",
            ""id"": ""82a33d08-3658-4fe5-a3a7-70899cc256b6"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""223e9b5c-a231-422e-986e-72d5af8c4a25"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1bc48cb-738b-4b2a-820f-0039e782ce77"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""04feb804-f305-4d9c-bbf6-e24781683367"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""0a28167c-3737-40a3-bcfd-e976f22e7441"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""042224d9-74c7-43c3-88d1-75a1867f3b6b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Escape"",
            ""id"": ""76f520b0-e27d-4639-8e34-253846c80a73"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""432720a7-e638-4d72-985a-4645c43e5e99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5a531116-9c02-47ff-bbaf-08506ea49a67"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Move
            m_Move = asset.FindActionMap("Move", throwIfNotFound: true);
            m_Move_Move = m_Move.FindAction("Move", throwIfNotFound: true);
            // Look
            m_Look = asset.FindActionMap("Look", throwIfNotFound: true);
            m_Look_Look = m_Look.FindAction("Look", throwIfNotFound: true);
            // Interaction
            m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
            m_Interaction_Use = m_Interaction.FindAction("Use", throwIfNotFound: true);
            // Escape
            m_Escape = asset.FindActionMap("Escape", throwIfNotFound: true);
            m_Escape_Pause = m_Escape.FindAction("Pause", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Move
        private readonly InputActionMap m_Move;
        private IMoveActions m_MoveActionsCallbackInterface;
        private readonly InputAction m_Move_Move;
        public struct MoveActions
        {
            private @FirstPersonInputActions m_Wrapper;
            public MoveActions(@FirstPersonInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Move_Move;
            public InputActionMap Get() { return m_Wrapper.m_Move; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MoveActions set) { return set.Get(); }
            public void SetCallbacks(IMoveActions instance)
            {
                if (m_Wrapper.m_MoveActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_MoveActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_MoveActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_MoveActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_MoveActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                }
            }
        }
        public MoveActions @Move => new MoveActions(this);

        // Look
        private readonly InputActionMap m_Look;
        private ILookActions m_LookActionsCallbackInterface;
        private readonly InputAction m_Look_Look;
        public struct LookActions
        {
            private @FirstPersonInputActions m_Wrapper;
            public LookActions(@FirstPersonInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Look => m_Wrapper.m_Look_Look;
            public InputActionMap Get() { return m_Wrapper.m_Look; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(LookActions set) { return set.Get(); }
            public void SetCallbacks(ILookActions instance)
            {
                if (m_Wrapper.m_LookActionsCallbackInterface != null)
                {
                    @Look.started -= m_Wrapper.m_LookActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_LookActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_LookActionsCallbackInterface.OnLook;
                }
                m_Wrapper.m_LookActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                }
            }
        }
        public LookActions @Look => new LookActions(this);

        // Interaction
        private readonly InputActionMap m_Interaction;
        private IInteractionActions m_InteractionActionsCallbackInterface;
        private readonly InputAction m_Interaction_Use;
        public struct InteractionActions
        {
            private @FirstPersonInputActions m_Wrapper;
            public InteractionActions(@FirstPersonInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Use => m_Wrapper.m_Interaction_Use;
            public InputActionMap Get() { return m_Wrapper.m_Interaction; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
            public void SetCallbacks(IInteractionActions instance)
            {
                if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
                {
                    @Use.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnUse;
                    @Use.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnUse;
                    @Use.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnUse;
                }
                m_Wrapper.m_InteractionActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Use.started += instance.OnUse;
                    @Use.performed += instance.OnUse;
                    @Use.canceled += instance.OnUse;
                }
            }
        }
        public InteractionActions @Interaction => new InteractionActions(this);

        // Escape
        private readonly InputActionMap m_Escape;
        private IEscapeActions m_EscapeActionsCallbackInterface;
        private readonly InputAction m_Escape_Pause;
        public struct EscapeActions
        {
            private @FirstPersonInputActions m_Wrapper;
            public EscapeActions(@FirstPersonInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Pause => m_Wrapper.m_Escape_Pause;
            public InputActionMap Get() { return m_Wrapper.m_Escape; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(EscapeActions set) { return set.Get(); }
            public void SetCallbacks(IEscapeActions instance)
            {
                if (m_Wrapper.m_EscapeActionsCallbackInterface != null)
                {
                    @Pause.started -= m_Wrapper.m_EscapeActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_EscapeActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_EscapeActionsCallbackInterface.OnPause;
                }
                m_Wrapper.m_EscapeActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                }
            }
        }
        public EscapeActions @Escape => new EscapeActions(this);
        public interface IMoveActions
        {
            void OnMove(InputAction.CallbackContext context);
        }
        public interface ILookActions
        {
            void OnLook(InputAction.CallbackContext context);
        }
        public interface IInteractionActions
        {
            void OnUse(InputAction.CallbackContext context);
        }
        public interface IEscapeActions
        {
            void OnPause(InputAction.CallbackContext context);
        }
    }
}
