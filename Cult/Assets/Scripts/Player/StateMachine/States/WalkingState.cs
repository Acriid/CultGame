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
    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
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
