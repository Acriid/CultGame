using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputManager : MonoBehaviour
{
    public CustomInputSystem inputActions;
    public static InputManager instance { get; private set; }
    public static InputDevice LastUsedDevice { get; private set; }
    public static event System.Action<InputDevice> OnDeviceChanged;
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Debug.LogWarning("More than one instance of InputManager.");
            }
        }
        instance = this;
        InitializeInput();
    }
    void OnEnable()
    {
        InitializeInput();
        InputSystem.onAnyButtonPress.Call(control =>
        {
            if (LastUsedDevice != control.device)
            {
                LastUsedDevice = control.device;
                OnDeviceChanged?.Invoke(LastUsedDevice);
            }
        });
    }
    void OnDisable()
    {
        CleanUpInput();
    }

    void OnDestroy()
    {
        CleanUpInput();
    }

    void InitializeInput()
    {
        if (inputActions == null)
        {
            inputActions = new CustomInputSystem();
            inputActions.Enable();
        }
    }
    void CleanUpInput()
    {
        if (inputActions != null)
        {
            inputActions.Dispose();
            inputActions.Disable();
            inputActions = null;
        }
    }
    public void StopInput()
    {
        CleanUpInput();
    }
    public void StartInput()
    {
        InitializeInput();
    }
    public string GetBindingDisplay(InputAction action)
    {
        if (action == null || LastUsedDevice == null)
            return "";

        string layout = LastUsedDevice.layout.ToLower();

        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];
            if (binding.isComposite) continue;

            if (binding.path.ToLower().Contains(layout))
            {
                return action.GetBindingDisplayString(
                    i,
                    InputBinding.DisplayStringOptions.DontIncludeInteractions
                );
            }
        }

        return action.GetBindingDisplayString(InputBinding.DisplayStringOptions.DontIncludeInteractions);
    }


}
