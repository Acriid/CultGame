using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public float playerSpeed = 1f;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    private CharacterController characterController;
    public PlayerStateMachine playerStateMachine { get; set; }
    public WalkingState walkingState { get; set; }
    public CrouchingState crouchingState { get; set; }
    private Vector3 velocity = Vector3.zero;
    const float gravity = -9.81f;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStateMachine.Initialize(walkingState);
    }
    void Awake()
    {
        playerStateMachine = new PlayerStateMachine();
        walkingState = new WalkingState(this, playerStateMachine);
        crouchingState = new CrouchingState(this, playerStateMachine);
    }
    void Update()
    {
        playerStateMachine.CurrentPlayerState.UpdateLogic();
    }
    void FixedUpdate()
    {
        playerStateMachine.CurrentPlayerState.FixedUpdateLogic();
    }
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
}
