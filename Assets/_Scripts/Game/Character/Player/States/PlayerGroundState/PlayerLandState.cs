using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{ 
    protected float _moveSpeed = 5;
    private int _accelerationFrames = 6; // 加速にかかるフレーム数
    private float _targetVelocityX => player.XInput * _moveSpeed;         // 目標のX軸上の速度
    public PlayerLandState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ParticleData.FallParticle.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if(!playerStateMachine.CheckCurrentState(this))
            return;
        
        if (player.StateData.HasJumpBuffer)
        {
            Debug.Log("Jump");
            playerStateMachine.ChangeState(PlayerStateEnum.Jump);
            return;
        }
        
        if (!player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
            return;
        }
        
        if (player.XInput == 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }
        
        // 入力があり、かつ地面にいる場合、移動状態に切り替える
        if (player.XInput != 0 && player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Run);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 加速しながら目標速度に向けて速度を変更
        if (player.XInput != 0)
        {
            ChangeVelocity(new Vector2(_targetVelocityX,player.Rigidbody2D.velocity.y) , _accelerationFrames);
        }
    }
}