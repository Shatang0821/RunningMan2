using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(string animBoolName, global::Player player, PlayerStateMachine playerStateMachine) :
        base(animBoolName, player, playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.FlipController(player.XInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    
}