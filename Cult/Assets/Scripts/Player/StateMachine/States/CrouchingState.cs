using UnityEngine;
using UnityEngine.Rendering;

public class CrouchingState : WalkingState
{
    const float speedDifference = 4f;
    public CrouchingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.SetPlayerSpeed(player.playerSpeed / speedDifference);
        player.transform.localScale = new Vector3(1f, 0.5f, 1f);
    }
    public override void ExitState()
    {
        base.ExitState();
        player.SetPlayerSpeed(player.playerSpeed * speedDifference);
        player.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
    public override void FixedUpdateLogic()
    {
        base.FixedUpdateLogic();
    }
}
