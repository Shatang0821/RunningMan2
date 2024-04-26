
    using UnityEngine;

    public class PlayerCoyoteState : PlayerGroundedState
    {
        private float _coyoteTime = 0.1f;
        
        private float _moveSpeed = 6;
        private int _accelerationFrames = 6; // 加速にかかるフレーム数
        private float _targetVelocityX => player.XInput * _moveSpeed;         // 目標のX軸上の速度
        
        public PlayerCoyoteState(string animBoolName, Player player, PlayerStateMachine playerStateMachine) : base(animBoolName, player, playerStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            player.SetGravity(0);
            stateTimer = _coyoteTime;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            // 現在の状態がこの状態でなければ、さらなるロジックを実行しない
            if(playerStateMachine.CurrentState != this)
                return;
            
            // 地面が検出されているかチェック
            if (player.IsGroundDetected())
            {
                // 地面にいる場合の入力処理
                if (player.XInput != 0)
                {
                    playerStateMachine.ChangeState(PlayerStateEnum.Run);
                }
                else
                {
                    playerStateMachine.ChangeState(PlayerStateEnum.Idle);
                }
            }
            else if (stateTimer > 0)
            {
                // coyoteTime中で地面にいない場合でもジャンプが許可される
                // ここでジャンプ入力を確認するロジックがあれば良い
                if (player.IsJump)
                {
                    playerStateMachine.ChangeState(PlayerStateEnum.Jump);
                }
            }
            else
            {
                // coyoteTimeが終了していても地面が検出されない場合、Fall状態に遷移
                playerStateMachine.ChangeState(PlayerStateEnum.Fall);
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
