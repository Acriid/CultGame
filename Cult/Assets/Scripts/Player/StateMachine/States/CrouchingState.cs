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
        player.animator.SetBool("Crouching", true);
        player.SetPlayerSpeed(player.playerSpeed / speedDifference);
        player.SetCharacterControllerHeight(player.characterController.height/2f);
    }
    public override void ExitState()
    {
        player.animator.SetBool("Crouching", false);
        player.SetPlayerSpeed(player.playerSpeed * speedDifference);
        player.SetCharacterControllerHeight(player.characterController.height * 2f);
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
