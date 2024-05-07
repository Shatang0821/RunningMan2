using UnityEngine;

public class PlayerWallJumpState : PlayerWallState
{
    private readonly float _totalJumpTime = 0.3f; // 上昇の持続フレーム数
    private readonly Vector2 _jumpVelocity = new Vector2(4.8f, 10f);

    public PlayerWallJumpState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravity(4);
        
        player.SetVelocityY(_jumpVelocity.y);
        
        stateTimer = _totalJumpTime;
        player.ParticleData.JumpParticle.Play();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
        if (!playerStateMachine.CheckCurrentState(this))
            return;
        
        // ジャンプの持続時間が終了したら、落下状態に切り替える
        if (stateTimer < 0)
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Fall);
            return;
        }
    }

    public override void PhysicsUpdate()
    {
        //base.PhysicsUpdate();
        // 壁ジャンプ時のX軸方向の速度を設定（プレイヤーの向きに依存）
        player.SetVelocityX(_jumpVelocity.x * player.facingDir);
    }
}