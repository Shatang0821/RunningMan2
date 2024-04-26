﻿using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(string animBoolName, global::Player player, PlayerStateMachine playerStateMachine) :
        base(animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(1);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.FlipController(player.XInput);
        
        // ジャンプ入力がある場合、ジャンプ状態に切り替える
        if (player.IsJump)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Jump);
            return;
        }

        if (!player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.CoyoteTime);
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