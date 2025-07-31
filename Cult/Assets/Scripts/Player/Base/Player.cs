using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public float playerSpeed;
    [Header("Oriantation Transform")]
    [SerializeField] public Transform oriantation;
    private Rigidbody playerRigidBody;
    public PlayerStateMachine playerStateMachine;
    public WalkingState walkingState;
    void Awake()
    {
        
    }
    public void MovePlayer(Vector2 Direction)
    {
        Vector3 moveDirection = oriantation.forward * Direction.y + oriantation.right * Direction.x;
        playerRigidBody.linearVelocity = moveDirection * playerSpeed;
    }
}
