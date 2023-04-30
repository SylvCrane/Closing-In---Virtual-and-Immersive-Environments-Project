//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.0
//     from Assets/Demos/Scripts/Input/PlayerLooker.inputactions
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

public partial class @PlayerLooker: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerLooker()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerLooker"",
    ""maps"": [
        {
            ""name"": ""Looker"",
            ""id"": ""1c42b3f3-bd8a-45c0-88b9-948b840e914d"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""c909e02b-18d4-4778-9377-17c9bdbe5906"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5549a30c-9cc8-479e-9d5a-412260cce42c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Looker
        m_Looker = asset.FindActionMap("Looker", throwIfNotFound: true);
        m_Looker_Look = m_Looker.FindAction("Look", throwIfNotFound: true);
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

    // Looker
    private readonly InputActionMap m_Looker;
    private List<ILookerActions> m_LookerActionsCallbackInterfaces = new List<ILookerActions>();
    private readonly InputAction m_Looker_Look;
    public struct LookerActions
    {
        private @PlayerLooker m_Wrapper;
        public LookerActions(@PlayerLooker wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Looker_Look;
        public InputActionMap Get() { return m_Wrapper.m_Looker; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LookerActions set) { return set.Get(); }
        public void AddCallbacks(ILookerActions instance)
        {
            if (instance == null || m_Wrapper.m_LookerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LookerActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
        }

        private void UnregisterCallbacks(ILookerActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
        }

        public void RemoveCallbacks(ILookerActions instance)
        {
            if (m_Wrapper.m_LookerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILookerActions instance)
        {
            foreach (var item in m_Wrapper.m_LookerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LookerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LookerActions @Looker => new LookerActions(this);
    public interface ILookerActions
    {
        void OnLook(InputAction.CallbackContext context);
    }
}
