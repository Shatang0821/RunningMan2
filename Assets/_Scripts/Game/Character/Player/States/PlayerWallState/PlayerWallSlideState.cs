public class PlayerWallSlideState : PlayerWallState
{
    public PlayerWallSlideState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        player.SetGravity(0.2f);
        player.ParticleData.TouchParticle.Play();
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

        // 壁に接触していて、ジャンプ入力がある場合、壁ジャンプ状態に切り替える
        if (player.JumpInput && player.IsWallDetected())
        {
            //反対側にジャンプするため向きを変更
            player.Flip();
            playerStateMachine.ChangeState(PlayerStateEnum.WallJump);
            return;
        }
        
        if (player.ClimbInput)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Climb);
            return;
        }
    }
}