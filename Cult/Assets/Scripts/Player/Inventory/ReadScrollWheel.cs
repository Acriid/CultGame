using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class ReadScrollWheel : MonoBehaviour
{
    public InputActionReference scrollInput;
    float CurrentSelected = 0f;
    void Awake()
    {
        scrollInput.action.performed += ReadScrollValue;
    }
    void OnDisable()
    {
        scrollInput.action.performed -= ReadScrollValue;
    }

    private void ReadScrollValue(InputAction.CallbackContext ctx)
    {
        Vector2 ScrollValue = ctx.ReadValue<Vector2>();
        CurrentSelected += ScrollValue.y;
        CurrentSelected = Mathf.Clamp(CurrentSelected, 0f, 3f);
        Debug.Log(CurrentSelected);
    }
}
