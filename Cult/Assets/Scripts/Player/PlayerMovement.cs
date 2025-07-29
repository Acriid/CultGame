using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public float moveSpeed;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    //MovementInputs
    private float horizontalInput;
    private float verticalInput;
    //MoveDirection
    private Vector3 moveDirection;
    //RigidBody
    private Rigidbody rb;
    //Input
    private InputAction moveInputs;
    void Awake()
    {
        GetRigidBody();
        InitializeInput();
    }
    void OnEnable()
    {
        GetRigidBody();
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
        GetInput();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    void InitializeInput()
    {
        //InputActions
        if (moveInputs == null)
        {
            moveInputs = InputManager.instance.inputActions.Player.Move;
            moveInputs.Enable();
        }
    }
    void CleanUpInput()
    {
        //MoveInputs
        if (moveInputs != null)
        {
            moveInputs.Dispose();
            moveInputs.Disable();
            moveInputs = null;
        }
    }

    void GetInput()
    {
        //Reads inputs from the controls
        horizontalInput = moveInputs.ReadValue<Vector2>().x;
        verticalInput = moveInputs.ReadValue<Vector2>().y;
    }

    void MovePlayer()
    {
        //Moves towards oriantation
        moveDirection = oriantation.forward * verticalInput + oriantation.right * horizontalInput;
        //Add Force used for an acceleration type movement.
        rb.linearVelocity = moveDirection * moveSpeed;
        //rb.AddForce(moveDirection * moveSpeed * 10, ForceMode.Force);
    }

    void GetRigidBody()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }
    }
}