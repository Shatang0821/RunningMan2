using FrameWork.Utils;


public class PlayerIdleState : PlayerGroundedState
{
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
        if(player.XInput != 0)
            playerStateMachine.ChangeState(PlayerStateEnum.Run);
        if(player.IsJump)
            playerStateMachine.ChangeState(PlayerStateEnum.Jump);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}