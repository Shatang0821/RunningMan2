using UnityEngine;


public class PlayerFallState : PlayerAirState
{
    private float maxFallSpeed = -15;        //落下の最大速度を制限する
    public PlayerFallState(string animBoolName, global::Player player, PlayerStateMachine playerStateMachine) :
        base(animBoolName, player, playerStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _moveSpeed = 4;
        player.SetGravity(4);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.IsGroundDetected())
        {
            playerStateMachine.ChangeState(PlayerStateEnum.Idle);
            return;
        }
            
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        // 落下速度を制限する処理。速度が最大落下速度を超えないように調整
        float newFallSpeed = Mathf.Clamp(player.Rigidbody2D.velocity.y, maxFallSpeed, float.MaxValue);
        player.Rigidbody2D.velocity = new Vector2(player.Rigidbody2D.velocity.x, newFallSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }
}