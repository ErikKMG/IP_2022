//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputActions/PauseInputs.inputactions
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

public partial class @PauseInputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PauseInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseInputs"",
    ""maps"": [
        {
            ""name"": ""Pause"",
            ""id"": ""b6d7731e-fba6-4f8b-a27d-d4ad69890d5f"",
            ""actions"": [
                {
                    ""name"": ""PauseKey"",
                    ""type"": ""Button"",
                    ""id"": ""2b35743d-c377-47fb-b5c2-0c228f270fea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""84efef2a-2e1b-4f6c-8fed-769e28a61756"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_PauseKey = m_Pause.FindAction("PauseKey", throwIfNotFound: true);
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

    // Pause
    private readonly InputActionMap m_Pause;
    private IPauseActions m_PauseActionsCallbackInterface;
    private readonly InputAction m_Pause_PauseKey;
    public struct PauseActions
    {
        private @PauseInputs m_Wrapper;
        public PauseActions(@PauseInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseKey => m_Wrapper.m_Pause_PauseKey;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterface != null)
            {
                @PauseKey.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseKey;
                @PauseKey.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseKey;
                @PauseKey.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnPauseKey;
            }
            m_Wrapper.m_PauseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseKey.started += instance.OnPauseKey;
                @PauseKey.performed += instance.OnPauseKey;
                @PauseKey.canceled += instance.OnPauseKey;
            }
        }
    }
    public PauseActions @Pause => new PauseActions(this);
    public interface IPauseActions
    {
        void OnPauseKey(InputAction.CallbackContext context);
    }
}
