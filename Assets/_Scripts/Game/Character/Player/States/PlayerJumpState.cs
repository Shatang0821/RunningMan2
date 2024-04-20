using FrameWork.Utils;
using UnityEngine;


public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }

    private int _accelerationFrames = 12;

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(20);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (currentFrame >= _accelerationFrames)
            playerStateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}