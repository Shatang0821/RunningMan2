using UnityEngine;


public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(string animBoolName, global::Player player, PlayerStateMachine playerStateMachine) :
        base(animBoolName, player, playerStateMachine)
    {
    }

    private int _accelerationFrames = 12;
    private Vector3 _minVelocity => new Vector3(player.Physics.Velocity.x, -20, 0);

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGroundDetected())
            playerStateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        ChangeVelocity(_minVelocity, _accelerationFrames);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocityY(0);
    }
}