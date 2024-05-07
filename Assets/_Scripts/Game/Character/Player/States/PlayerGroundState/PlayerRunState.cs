using FrameWork.FSM;
using FrameWork.Utils;
using UnityEngine;


public class PlayerRunState : PlayerGroundedState
{
    private float _moveSpeed = 5;
    private int _accelerationFrames = 6; // 加速にかかるフレーム数
    private float _targetVelocityX => player.XInput * _moveSpeed; // 目標のX軸上の速度
    private float _counter; // 移動パーティクルエフェクトのタイマー

    public PlayerRunState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _counter = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if (!playerStateMachine.CheckCurrentState(this))
            return;
        // 移動パーティクルエフェクトのタイマーを更新
        _counter += Time.deltaTime;
        
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if (!playerStateMachine.CheckCurrentState(this))
            return;

        if (player.XInput == 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 加速しながら目標速度に向けて速度を変更
        if (player.XInput != 0)
        {
            ChangeVelocity(new Vector2(_targetVelocityX, player.Rigidbody2D.velocity.y), _accelerationFrames);
        }
        // 移動パーティクルエフェクトの再生間隔を制御
        if (_counter > 0.1)
        {
            player.ParticleData.MovementParticle.Play();
            _counter = 0;
        }
    }
}