using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class UpdateTextInteract : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    const string Actionname = "Move";

    private void OnEnable()
    {
        InputManager.OnDeviceChanged += UpdateUI;
        UpdateUI(InputManager.LastUsedDevice);
    }

    private void OnDisable()
    {
        InputManager.OnDeviceChanged -= UpdateUI;
    }

    private void UpdateUI(InputDevice device)
    {
        if (InputManager.instance == null || device == null) return;

        var action = InputManager.instance.inputActions.Player.Interact;
        label.text = InputManager.instance.GetBindingDisplay(action);
    }
}
