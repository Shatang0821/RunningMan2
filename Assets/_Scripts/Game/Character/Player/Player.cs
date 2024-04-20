using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck; //地面チェック
    [SerializeField] private float _groundCheckDistance; //チェック距離
    [SerializeField] private Transform _wallCheck; //壁チェック
    [SerializeField] private float _wallCheckDistance; //チェック距離
    [SerializeField] private LayerMask _whatIsGround; //レイヤー設定

    /// <summary>
    /// 向いている方向
    /// </summary>
    public int FacingDir { get; private set; } = 1;

    private bool _facingRight = true;

    public Physics Physics;
    private PlayerInput _playerInput;
    private PlayerStateMachine _playerStateMachine;

    public float XInput => _playerInput.Axis.x;
    public float YInput => _playerInput.Axis.y;
    public bool IsJump => _playerInput.Jump;
    public bool IsStopJump => _playerInput.StopJump;
    
    #region LifeMethod

    private void Awake()
    {
        this.Physics = new Physics(this.transform);
        this._playerInput = new PlayerInput();

        _playerStateMachine = new PlayerStateMachine(this);
        _playerStateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    private void OnEnable()
    {
        _playerInput.OnEnable();
        this.transform.position = Vector2.zero;
    }

    private void OnDisable()
    {
        _playerInput.OnDisable();
    }

    public void Update()
    {
        _playerStateMachine.LogicUpdate();
    }

    public void FixedUpdate()
    {
        _playerStateMachine.PhysicsUpdate();
        Physics.UpdatePosition();
    }

    #endregion


    public void SetVelocityX(float x)
    {
        Physics.Velocity.x = x;
    }
    public void SetVelocityY(float y)
    {
        Physics.Velocity.y = y;
    }
    
    public void SetUseGravity(float value)
    {
        //Physics.Gravity = value;
    }

    #region 判定

    /// <summary>
    /// 地面チェック
    /// </summary>
    /// <returns>true or false</returns>
    public bool IsGroundDetected() =>
        Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);

    public virtual bool IsWallDetected() => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDir,
        _wallCheckDistance, _whatIsGround);

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position,
            new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position,
            new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
    }

    #endregion

    public void SetVelocity(Vector3 currentVelocity)
    {
        Physics.Velocity = currentVelocity;
    }
}