using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : PlayerState
{
    public WalkingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.InitializeJumpAction();
    }
    public override void ExitState()
    {
        base.ExitState();
        player.CleanUpJumpAction();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        player.CheckIfCanJump();
    }
    public override void FixedUpdateLogic()
    {
        base.FixedUpdateLogic();
        player.MovePlayer(player.moveInputValue);
    }
    public override void AnimationTriggerEvent()
    {
        base.AnimationTriggerEvent();
    }
        
}
