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
    //InputActions
    private InputSystem_Actions inputActions;
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
        //InputActions
        if (inputActions == null)
        {
            inputActions = new InputSystem_Actions();
        }
        //lookInput
        if (lookInput == null)
        {
            lookInput = inputActions.Player.Look;
            lookInput.Enable();
        }

    }
    void CleanUpInput()
    {
        //lookInput
        if (lookInput != null)
        {
            lookInput.Dispose();
            lookInput.Disable();
            lookInput = null;
        }
        //InputActions
        if (inputActions != null)
        {
            inputActions.Dispose();
            inputActions.Disable();
            inputActions = null;
        }
    }

    void RotateCamera()
    {
        //Mouse Input
        float lookX = lookInput.ReadValue<Vector2>().x * LookSensitivity * Time.deltaTime;
        float lookY = lookInput.ReadValue<Vector2>().y * LookSensitivity * Time.deltaTime;

        //Change the rotation
        yRotation += lookX;
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -MaxLookRange, MaxLookRange);

        //Rotate 
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        oriantation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
