//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Player/Input/PlayerInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""b754a949-bddc-42ea-9b26-4f661cdac9db"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""17ef01ee-4e67-4cdc-8b70-9d32a3673a3a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Grapple"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d082f82c-2491-4d7d-a92d-4fb364ff3538"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LookX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1c1f7006-0754-4f8a-bfd1-38b99c47b37b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LookY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1d0b78ce-44a3-4876-a7cc-ca8cf5204f20"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fa8a85be-0c52-4630-a20b-14d53187bca6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3f346d99-fefa-4696-b60b-94b51ac953f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""51cae671-bfc2-47fd-bcc8-149818b5eff0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8388add2-babb-496d-bf90-b43731cbbd49"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b9df7bc3-4ee1-426a-8d4b-d0fe98c97564"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2a64b745-6b5e-4079-b921-e9f93bdf90da"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1e9c47bc-4c58-42db-955d-da82ca113fdd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""018f6e57-607f-4ed7-90a9-c34f0f346e7f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c56fee70-cb0d-4b9f-9736-40351da736a7"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb271f62-4a2f-4cb5-b143-cb9ee6e057f7"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LookY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65f0ffeb-fb0f-4d1d-b836-152b991a35f6"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8b61f0a-cd9e-48ab-82f9-08b678938999"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Testing"",
            ""id"": ""2b0540de-d0ee-4146-8a5a-0903716811c8"",
            ""actions"": [
                {
                    ""name"": ""Test"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6b1de364-513f-4515-97bc-d8304376f02e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7fa56243-9548-4d70-95f4-931b8789f70f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Test"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Movement = m_PlayerControls.FindAction("Movement", throwIfNotFound: true);
        m_PlayerControls_Grapple = m_PlayerControls.FindAction("Grapple", throwIfNotFound: true);
        m_PlayerControls_LookX = m_PlayerControls.FindAction("LookX", throwIfNotFound: true);
        m_PlayerControls_LookY = m_PlayerControls.FindAction("LookY", throwIfNotFound: true);
        m_PlayerControls_Sprint = m_PlayerControls.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerControls_Shoot = m_PlayerControls.FindAction("Shoot", throwIfNotFound: true);
        // Testing
        m_Testing = asset.FindActionMap("Testing", throwIfNotFound: true);
        m_Testing_Test = m_Testing.FindAction("Test", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Movement;
    private readonly InputAction m_PlayerControls_Grapple;
    private readonly InputAction m_PlayerControls_LookX;
    private readonly InputAction m_PlayerControls_LookY;
    private readonly InputAction m_PlayerControls_Sprint;
    private readonly InputAction m_PlayerControls_Shoot;
    public struct PlayerControlsActions
    {
        private @PlayerInputs m_Wrapper;
        public PlayerControlsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerControls_Movement;
        public InputAction @Grapple => m_Wrapper.m_PlayerControls_Grapple;
        public InputAction @LookX => m_Wrapper.m_PlayerControls_LookX;
        public InputAction @LookY => m_Wrapper.m_PlayerControls_LookY;
        public InputAction @Sprint => m_Wrapper.m_PlayerControls_Sprint;
        public InputAction @Shoot => m_Wrapper.m_PlayerControls_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                @Grapple.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGrapple;
                @Grapple.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGrapple;
                @Grapple.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGrapple;
                @LookX.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookX;
                @LookX.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookX;
                @LookX.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookX;
                @LookY.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookY;
                @LookY.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookY;
                @LookY.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLookY;
                @Sprint.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSprint;
                @Shoot.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Grapple.started += instance.OnGrapple;
                @Grapple.performed += instance.OnGrapple;
                @Grapple.canceled += instance.OnGrapple;
                @LookX.started += instance.OnLookX;
                @LookX.performed += instance.OnLookX;
                @LookX.canceled += instance.OnLookX;
                @LookY.started += instance.OnLookY;
                @LookY.performed += instance.OnLookY;
                @LookY.canceled += instance.OnLookY;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);

    // Testing
    private readonly InputActionMap m_Testing;
    private ITestingActions m_TestingActionsCallbackInterface;
    private readonly InputAction m_Testing_Test;
    public struct TestingActions
    {
        private @PlayerInputs m_Wrapper;
        public TestingActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Test => m_Wrapper.m_Testing_Test;
        public InputActionMap Get() { return m_Wrapper.m_Testing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestingActions set) { return set.Get(); }
        public void SetCallbacks(ITestingActions instance)
        {
            if (m_Wrapper.m_TestingActionsCallbackInterface != null)
            {
                @Test.started -= m_Wrapper.m_TestingActionsCallbackInterface.OnTest;
                @Test.performed -= m_Wrapper.m_TestingActionsCallbackInterface.OnTest;
                @Test.canceled -= m_Wrapper.m_TestingActionsCallbackInterface.OnTest;
            }
            m_Wrapper.m_TestingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Test.started += instance.OnTest;
                @Test.performed += instance.OnTest;
                @Test.canceled += instance.OnTest;
            }
        }
    }
    public TestingActions @Testing => new TestingActions(this);
    public interface IPlayerControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnGrapple(InputAction.CallbackContext context);
        void OnLookX(InputAction.CallbackContext context);
        void OnLookY(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
    public interface ITestingActions
    {
        void OnTest(InputAction.CallbackContext context);
    }
}
