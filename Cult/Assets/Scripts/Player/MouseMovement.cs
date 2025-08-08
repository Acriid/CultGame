using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] public float LookSensitivity = 200f;
    [SerializeField] public float MaxLookRange = 90f;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    //Rotations
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float lookX = 0f;
    private float lookY = 0f;
    //InputActions
    private InputAction lookInput;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InitializeInput();
    }
    void OnEnable()
    {
        InitializeInput();
    }
    void OnDisable()
    {
        CleanUpInput();
    }

    void OnDestroy()
    {
        CleanUpInput();
    }
    void Update()
    {
        RotateCamera();
    }
    void InitializeInput()
    {
        //lookInput
        if (lookInput == null)
        {
            lookInput = InputManager.instance.inputActions.Player.Look;
            lookInput.started += LookMovement;
            lookInput.performed += LookMovement;
            lookInput.canceled += LookMovement;
            lookInput.Enable();
        }

    }
    void CleanUpInput()
    {
        //lookInput
        if (lookInput != null)
        {
            lookInput.started -= LookMovement;
            lookInput.performed -= LookMovement;
            lookInput.canceled -= LookMovement;
            lookInput.Dispose();
            lookInput.Disable();
            lookInput = null;
        }
    }
    void LookMovement(InputAction.CallbackContext ctx)
    {
        //Mouse Input
        lookX = ctx.ReadValue<Vector2>().x * LookSensitivity * Time.deltaTime;
        lookY = ctx.ReadValue<Vector2>().y * LookSensitivity * Time.deltaTime;

        //Change the rotation
        yRotation += lookX;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -MaxLookRange, MaxLookRange);
    }
    void RotateCamera()
    {
        //Rotate 
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        oriantation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
