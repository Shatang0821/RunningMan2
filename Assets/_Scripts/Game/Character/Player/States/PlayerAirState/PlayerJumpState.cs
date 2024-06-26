﻿using FrameWork.Utils;
using UnityEngine;


public class PlayerJumpState : PlayerAirState
{
    private float _totalJumpTime = 0.15f; // 上昇の持続フレーム数
    private float _initialVelocityY = 10f; // Y軸上の初速
    public PlayerJumpState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        player.SetGravity(3);
        player.SetVelocityY(_initialVelocityY);
        stateTimer = _totalJumpTime;
        player.ParticleData.JumpParticle.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if(!playerStateMachine.CheckCurrentState(this))
            return;
        
        if (player.Rigidbody2D.velocity.y <= 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
            return;
        }
        
        // 壁に接触していて、ジャンプ入力がある場合、壁ジャンプ状態に切り替える
        if ((player.StateData.HasJumpBuffer || player.JumpInput) && player.IsWallDetected() && stateTimer <0)
        {
            //向きを反転してから
            player.Flip();
            playerStateMachine.ChangeState(PlayerStateEnum.WallJump);
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