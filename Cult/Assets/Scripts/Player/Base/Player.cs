using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public float playerSpeed = 1f;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    [SerializeField] public Transform cameratransform;
    private CharacterController characterController;
    public PlayerStateMachine playerStateMachine { get; set; }
    public WalkingState walkingState { get; set; }
    public CrouchingState crouchingState { get; set; }
    private Vector3 velocity = Vector3.zero;
    private InputAction crouchInput;
    private InputAction moveInput;
    public Vector2 moveInputValue { get; private set; }
    const float gravity = -9.81f;
    #region Basic Unity Functions
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStateMachine.Initialize(walkingState);
        InitializeInputs();
    }
    void Awake()
    {
        playerStateMachine = new PlayerStateMachine();
        walkingState = new WalkingState(this, playerStateMachine);
        crouchingState = new CrouchingState(this, playerStateMachine);
    }
    void OnDisable()
    {
        CleanUpInputs();
    }
    void OnDestroy()
    {
        CleanUpInputs();
    }
    void Update()
    {
        playerStateMachine.CurrentPlayerState.UpdateLogic();
    }
    void FixedUpdate()
    {
        playerStateMachine.CurrentPlayerState.FixedUpdateLogic();
    }
    #endregion
    #region InputActions
    #region All Inputs
    private void InitializeInputs()
    {
        InitializeCrouchInput();
        InitializeMoveInput();

    }
    private void CleanUpInputs()
    {
        CleanUpCrouchInput();
        CleanUpMoveInput();
    }
    #endregion
    #region CrouchInput
    private void InitializeCrouchInput()
    {
        if (crouchInput == null)
        {
            crouchInput = InputManager.instance.inputActions.Player.Crouch;
            crouchInput.started += StartCrouch;
            crouchInput.canceled += CancelCrouch;
            crouchInput.Enable();
        }
    }
    private void CleanUpCrouchInput()
    {
        if (crouchInput != null)
        {
            crouchInput.started -= StartCrouch;
            crouchInput.canceled -= CancelCrouch;
            crouchInput.Dispose();
            crouchInput.Disable();
            crouchInput = null;
        }
    }
    #endregion
    #region MoveInput
    private void InitializeMoveInput()
    {
        if (moveInput == null)
        {
            moveInput = InputManager.instance.inputActions.Player.Move;
            moveInput.started += GetMoveValues;
            moveInput.performed += GetMoveValues;
            moveInput.canceled += GetMoveValues;
            moveInput.Enable();
        }
    }
    private void CleanUpMoveInput()
    {
        if (moveInput != null)
        {
            moveInput.started -= GetMoveValues;
            moveInput.performed -= GetMoveValues;
            moveInput.canceled -= GetMoveValues;
            moveInput.Dispose();
            moveInput.Disable();
            moveInput = null;
        }
    }
    #endregion
    #endregion
    #region All Actions
    #region CrouchAction
    private void StartCrouch(InputAction.CallbackContext ctx)
    {
        if (playerStateMachine.CurrentPlayerState == walkingState)
        {
            playerStateMachine.ChangeState(crouchingState);
        }
    }
    private void CancelCrouch(InputAction.CallbackContext ctx)
    {
        if (playerStateMachine.CurrentPlayerState == crouchingState)
        {
            playerStateMachine.ChangeState(walkingState);
        }
    }
    #endregion
    #region MoveAction
    void GetMoveValues(InputAction.CallbackContext ctx)
    {
        moveInputValue = ctx.ReadValue<Vector2>();
    }
    #endregion
    #endregion
    #region Movement
    public void MovePlayer(Vector2 Direction)
    {
        Vector3 moveDirection = oriantation.forward * Direction.y + oriantation.right * Direction.x;
        characterController.Move(moveDirection * playerSpeed);

        if (characterController.isGrounded && velocity.y < 0f) { velocity.y = -2f; }
        velocity.y += gravity * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, gravity, 0f);

        characterController.Move(velocity);

    }
    public void SetPlayerSpeed(float newValue)
    {
        playerSpeed = newValue;
    }
    #endregion
}
