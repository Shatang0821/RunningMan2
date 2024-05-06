using FrameWork.Utils;
using UnityEngine;


public class PlayerIdleState : PlayerGroundedState
{
    private int _decelerationFrames = 3;
    public PlayerIdleState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }

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
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if(!playerStateMachine.CheckCurrentState(this))
            return;
        
        if (player.XInput != 0 && player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Run);
            return; 
        }

        if (!player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
            return;
        }
        
        //...
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 減速処理。現在のフレームと減速に要するフレーム数を基に速度を調整
        ChangeVelocity(Vector2.zero, _decelerationFrames);
    }
}