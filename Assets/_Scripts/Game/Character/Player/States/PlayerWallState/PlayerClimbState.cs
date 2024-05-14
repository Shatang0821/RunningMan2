
using UnityEngine;

public class PlayerClimbState : PlayerWallState
{
    // ジャンプしたい方向を示す
    private enum JumpDirection
    {
        /// <summary>
        /// 壁側
        /// </summary>
        TowardsWall = 1,    
        /// <summary>
        /// 壁との反対側
        /// </summary>
        AwayFromWall = -1
    }
    
    private float _climbSpeed = 3;      //登るスピード
    private JumpDirection _jumpDir;                             //ジャンプ方向
    public PlayerClimbState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ParticleData.TouchParticle.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        SetFacingDir();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if(!playerStateMachine.CheckCurrentState(this))
            return;
        if (!player.ClimbInput && player.IsGroundDetected())
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
        
        // プレイヤーがジャンプ中に壁を検出した場合。
        if (player.JumpInput && player.IsWallDetected())
        {
            // ジャンプ方向がプレイヤーの向いている方向と逆の場合
            if (_jumpDir == JumpDirection.AwayFromWall)
            {
                //プレイヤーを反転させて
                player.Flip();
                //PlayerWallJumpState状態に切り替える。
                playerStateMachine.ChangeState(PlayerStateEnum.WallJump);
                return;
            }
            //そうでなければプレイヤーのを反転せず
            else
            {
                playerStateMachine.ChangeState(PlayerStateEnum.WallJump);
                return;
            }
        }
        
        if(!player.ClimbInput && !player.IsGroundDetected() && player.XInput == 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
            return;
        }

        if (!player.ClimbInput && player.XInput == player.XInput && player.IsWallDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.WallSlide);
            return;
        }

        if (!player.IsWallDetected() && player.YInput > 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.ClimbLeap);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //壁中に左右に動かないため
        if(player.rb.velocity.x != 0)
            player.SetVelocityX(0);
        
        if(player.IsWallDetected() && _jumpDir == JumpDirection.TowardsWall)
            player.SetVelocityY(player.YInput * _climbSpeed);
        else
            player.SetVelocityY(0);
    }
    
    public override void Exit()
    {
        base.Exit();
    }
    
    void SetFacingDir()
    {
        // ジャンプ方向の設定...
        if (player.facingDir == player.XInput || player.XInput == 0)
        {
            _jumpDir = JumpDirection.TowardsWall;
        }
        else
        {
            _jumpDir = JumpDirection.AwayFromWall;
        }
        // アニメーションパラメータを設定して、ジャンプ時のプレイヤーの向きを決定する。
        player.anim.SetFloat("playerFacing", (float)_jumpDir);
    }
}