using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(string animBoolName, global::Player player, PlayerStateMachine playerStateMachine) :
        base(animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(0);
        if (!player.CanDash)
        {
            player.CanDash = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.FlipController(player.XInput);
        
        // ジャンプ入力がある場合、ジャンプ状態に切り替える
        if (player.JumpInput && player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Jump);
            return;
        }

        if (!player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.CoyoteTime);
            return;
        }

        if (player.IsWallDetected() && player.ClimbInput)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Climb);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}