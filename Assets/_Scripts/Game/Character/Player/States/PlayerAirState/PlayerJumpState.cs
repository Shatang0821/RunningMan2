using FrameWork.Utils;
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.Rigidbody2D.velocity.y <= 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
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