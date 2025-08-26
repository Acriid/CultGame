using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public float playerSpeed = 1f;
    const float PlayerSpeedOffset = 0.2f;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    [SerializeField] public Transform cameratransform;
    [Header("Menus")]
    [SerializeField] public GameObject OptionsMenu;
    private CharacterController characterController;
    public PlayerStateMachine playerStateMachine { get; set; }
    public WalkingState walkingState { get; set; }
    public CrouchingState crouchingState { get; set; }
    private Vector3 velocity = Vector3.zero;
    private InputAction crouchInput;
    private InputAction moveInput;
    private InputAction optionsAction;
    public Vector2 moveInputValue { get; private set; }
    const float gravity = -9.81f;
    #region Basic Unity Functions
    void Start()
    {
        if (OptionsMenu == null)
        {
            Debug.LogWarning("Please put the menu prefab in the scene.");
        }

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
        InitializeOptionsAction();

    }
    private void CleanUpInputs()
    {
        CleanUpCrouchInput();
        CleanUpMoveInput();
        CleanUpOptionsAction();
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
        }
    }
    private void CleanUpCrouchInput()
    {
        if (crouchInput != null)
        {
            crouchInput.started -= StartCrouch;
            crouchInput.canceled -= CancelCrouch;
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
        }
    }
    private void CleanUpMoveInput()
    {
        if (moveInput != null)
        {
            moveInput.started -= GetMoveValues;
            moveInput.performed -= GetMoveValues;
            moveInput.canceled -= GetMoveValues;
            moveInput = null;
        }
    }
    #endregion
    #region EscapeMenu
    private void InitializeOptionsAction()
    {
        if (optionsAction == null)
        {
            optionsAction = InputManager.instance.inputActions.Player.Options;
            optionsAction.performed += OpenOptionsMenu;
        }
    }
    private void CleanUpOptionsAction()
    {
        if (optionsAction != null)
        {
            optionsAction.performed -= OpenOptionsMenu;
            optionsAction = null;
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
    #region OptionsAction
    void OpenOptionsMenu(InputAction.CallbackContext ctx)
    {
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
        }
        else
        {
            OptionsMenu.SetActive(true);
        }
    }
    #endregion
    #region Movement
    public void MovePlayer(Vector2 Direction)
    {
        Vector3 moveDirection = oriantation.forward * Direction.y + oriantation.right * Direction.x;
        characterController.Move(moveDirection * playerSpeed * PlayerSpeedOffset);

        if (characterController.isGrounded && velocity.y < 0f) { velocity.y = -2f; }
        velocity.y += gravity * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, gravity, 0f);

        characterController.Move(velocity * PlayerSpeedOffset);

    }
    public void SetPlayerSpeed(float newValue)
    {
        playerSpeed = newValue;
    }
    #endregion
    public void SetCharacterControllerHeight(float newValue)
    {
        characterController.height = newValue;
    }
}
