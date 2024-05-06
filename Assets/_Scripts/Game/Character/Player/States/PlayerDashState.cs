using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private float _dashDuration = 0.15f;  // ダッシュの持続時間
    private float _dashSpeed = 20;     // ダッシュ時のスピード
    private Vector2 _dashDir;     // ダッシュ方向
    
    private Vector2 _facDashDir => new Vector2(player.facingDir, 0); //向く方向にダッシュ
    
    private float _dashEffectInterval = 0.02f; // ダッシュエフェクトの発生間隔
    private float _releaseTimer; // 次のエフェクト生成までのタイマー
    
    public PlayerDashState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // ダッシュ時間を設定
        stateTimer = _dashDuration;
        player.SetGravity(0);
        
        player.CanDash = false;
        
        // ダッシュ方向に速度を設定
        _dashDir = (player.InputVector == Vector2.zero) ? _facDashDir : player.InputVector.normalized;
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if(!playerStateMachine.CheckCurrentState(this))
            return;
        if(stateTimer > 0) return;

        if (player.IsGroundDetected())
        {
            if (player.XInput != 0)
            {
                playerStateMachine.ChangeState(PlayerStateEnum.Run);
            }
            else
            {
                playerStateMachine.ChangeState(PlayerStateEnum.Idle);
            }
            return;
        }

        if (!player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocity(_dashSpeed * _dashDir);
    }

    public override void Exit()
    {
        base.Exit();
        
        player.SetVelocity(Vector2.zero);
    }
}