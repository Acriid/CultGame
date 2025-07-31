using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : PlayerState
{
    private InputAction moveInput;
    private Vector2 moveInputValue;
    public WalkingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        InitializeMoveInput();
    }
    public override void ExitState()
    {
        base.ExitState();
        CleanUpMoveInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        moveInputValue = moveInput.ReadValue<Vector2>();
    }
    public override void FixedUpdateLogic()
    {
        base.FixedUpdateLogic();
        player.MovePlayer(moveInputValue);
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }

    private void InitializeMoveInput()
    {
        if (moveInput == null)
        {
            moveInput = InputManager.instance.inputActions.Player.Move;
            moveInput.Enable();
        }
    }
    private void CleanUpMoveInput()
    {
        if (moveInput != null)
        {
            moveInput.Dispose();
            moveInput.Disable();
            moveInput = null;
        }
    }
        
}
