public class PlayerWallSlideState : PlayerWallState
{
    public PlayerWallSlideState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetGravity(0.2f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGroundDetected())
        {
            if (player.XInput != 0)
            {
                playerStateMachine.ChangeState(PlayerStateEnum.Run);
            }
            else
            {
                playerStateMachine.ChangeState(PlayerStateEnum.Idle);
            }
            return;
        }

        if (player.XInput != player.XInput || player.XInput == 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
            return;
        }

        // if (player.JumpInput && player.IsWallDetected())
        // {
        //     player.Flip();
        // }
        if (player.ClimbInput)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Climb);
            return;
        }
    }
}