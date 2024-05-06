using UnityEngine;


public class PlayerFallState : PlayerAirState
{
    private float maxFallSpeed = -15;        //落下の最大速度を制限する
    public PlayerFallState(string animBoolName, global::Player player, PlayerStateMachine playerStateMachine) :
        base(animBoolName, player, playerStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _moveSpeed = 4;
        player.SetGravity(4);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if(!playerStateMachine.CheckCurrentState(this))
            return;
        if (player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Land);
            return;
        }
        // 壁に接触していて、登る入力がある場合、登り状態に切り替える
        if (player.IsWallDetected() && player.ClimbInput)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Climb);
            return;
        }
        // 壁に接触していて、入力方向がプレイヤーの向いている方向と一致する場合、壁滑り状態に切り替える
        if (player.IsWallDetected() && player.XInput == player.facingDir && !player.ClimbInput)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.WallSlide);
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
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 落下速度を制限する処理。速度が最大落下速度を超えないように調整
        float newFallSpeed = Mathf.Clamp(player.Rigidbody2D.velocity.y, maxFallSpeed, float.MaxValue);
        player.Rigidbody2D.velocity = new Vector2(player.Rigidbody2D.velocity.x, newFallSpeed);
    }
}