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
    [SerializeField] private GameObject _sprite;
    
    public Physics Physics;
    private PlayerInput _playerInput;
    private PlayerStateMachine _playerStateMachine;

    public float XInput => _playerInput.Axis.x;
    public float YInput => _playerInput.Axis.y;
    public bool IsJump => _playerInput.Jump;
    public bool IsStopJump => _playerInput.StopJump;
    
    /// <summary>
    /// 向いている方向
    /// </summary>
    public int FacingDir { get; private set; } = 1;

    private bool _facingRight = true;
    
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
    
    public void SetVelocity(Vector3 currentVelocity)
    {
        Physics.Velocity = currentVelocity;
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
    
    #region Flip
    /// <summary>
    /// プレイヤー反転関数
    /// </summary>
    public void Flip()
    {
        FacingDir = FacingDir * -1;
        _facingRight = !_facingRight;
        //y軸中心に180回転
        _sprite.transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// 反転判断する関数
    /// </summary>
    /// <param name="_x"></param>
    public void FlipController(float _x)
    {
        if (_x > 0 && !_facingRight)
            Flip();
        else if (_x < 0 && _facingRight)
            Flip();
    }
    #endregion
    
    

    
}