using UnityEngine;

public class CrouchingState : WalkingState
{
    public CrouchingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.SetPlayerSpeed(player.playerSpeed / 4f);
        player.cameratransform.position = new Vector3(player.transform.position.x,
        player.transform.position.y / 2, player.transform.position.z);
    }
    public override void ExitState()
    {
        base.ExitState();
        player.SetPlayerSpeed(player.playerSpeed * 4f);
        player.cameratransform.position = new Vector3(player.transform.position.x,
        player.transform.position.y *2, player.transform.position.z);
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
