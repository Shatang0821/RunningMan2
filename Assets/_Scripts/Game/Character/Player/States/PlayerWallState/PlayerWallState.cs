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
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if (!playerStateMachine.CheckCurrentState(this))
            return;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    public override void Exit()
    {
        base.Exit();
        //速度をリセット
        player.SetVelocity(Vector2.zero);
    }
    

}