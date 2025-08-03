using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public float playerSpeed;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    private CharacterController characterController;
    public PlayerStateMachine playerStateMachine { get; set; }
    public WalkingState walkingState { get; set; }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStateMachine.Initialize(walkingState);
    }
    void Awake()
    {
        playerStateMachine = new PlayerStateMachine();
        walkingState = new WalkingState(this, playerStateMachine);
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
    }
}
