using UnityEngine;

public class PlayerWallState : PlayerBaseState
{
    public PlayerWallState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        // 速度をゼロに設定
        player.SetVelocity(Vector2.zero);
        player.SetGravity(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    public override void Exit()
    {
        base.Exit();
        //速度をリセット
        player.SetVelocityY(0);
    }
    

}