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
        InitializeInput();
    }
    public override void ExitState()
    {
        base.ExitState();
        CleanUpInput();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
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

    private void InitializeInput()
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
    private void CleanUpInput()
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

    void GetMoveValues(InputAction.CallbackContext ctx)
    {
        moveInputValue = ctx.ReadValue<Vector2>();
    }
        
}
