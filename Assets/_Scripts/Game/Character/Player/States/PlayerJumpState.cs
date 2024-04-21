using FrameWork.Utils;
using UnityEngine;


public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }
    private int _decelerationFrames = 12;
    private Vector3 _maxVelocity => new Vector3( player.Physics.Velocity.x,0,0);
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(20);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Debug.Log(currentFrame);
        if (player.Physics.Velocity.y <= 0)
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        ChangeVelocity(_maxVelocity,_decelerationFrames);
    }
    
    public override void Exit()
    {
        base.Exit();
        player.SetVelocityY(0);
    }
}