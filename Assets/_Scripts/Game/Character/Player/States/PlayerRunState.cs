using FrameWork.FSM;
using FrameWork.Utils;
using UnityEngine;


public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }
    private float _moveSpeed = 5;
    private int _accelerationFrames = 12;   // 加速にかかるフレーム数
    private int _decelerationFrames = 6;
    
    private Vector3 _maxVelocity => new Vector3( player.XInput * _moveSpeed,player.Physics.Velocity.y,0);
    private Vector3 _minVelocity => new Vector3(0, player.Physics.Velocity.y, 0);
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(player.Physics.Velocity.x == 0 && player.XInput == 0)
            playerStateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (player.XInput != 0)
        {
            ChangeVelocity(_maxVelocity,_accelerationFrames);
        }
        else
        {
            ChangeVelocity(_minVelocity,_decelerationFrames);
        }
        
    }
    
   
}