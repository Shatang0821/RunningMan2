public class PlayerClimbLeapState : PlayerAirState
{
    //private float _totalJumpTime = 0.15f; // 上昇の持続フレーム数
    private float _initialVelocityY = 5f; // Y軸上の初速
    
    public PlayerClimbLeapState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(3);
        player.SetVelocityY(_initialVelocityY);
        player.ParticleData.JumpParticle.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if (!playerStateMachine.CheckCurrentState(this))
            return;

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