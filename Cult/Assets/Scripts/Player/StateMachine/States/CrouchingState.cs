using UnityEngine;
using UnityEngine.Rendering;

public class CrouchingState : WalkingState
{
    const float speedDifference = 4f;
    const float cameraMovement = 2f;
    public CrouchingState(Player player, PlayerStateMachine playerStateMachine) : base(player, playerStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
        player.SetPlayerSpeed(player.playerSpeed / speedDifference);
        player.cameratransform.position = new Vector3(player.transform.position.x,
        player.transform.position.y / cameraMovement, player.transform.position.z);
    }
    public override void ExitState()
    {
        base.ExitState();
        player.SetPlayerSpeed(player.playerSpeed * speedDifference);
        player.cameratransform.position = new Vector3(player.transform.position.x,
        player.transform.position.y * cameraMovement, player.transform.position.z);
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
