using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    protected float _moveSpeed = 5;
    private int _accelerationFrames = 6; // 加速にかかるフレーム数
    private float _targetVelocityX => player.XInput * _moveSpeed;         // 目標のX軸上の速度
    public PlayerAirState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(
        animBoolName, player, playerStateMachine)
    {
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