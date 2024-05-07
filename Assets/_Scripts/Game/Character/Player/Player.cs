using System;
using System.Collections;
using FrameWork.Utils;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class Player : Entity
{
    
    private PlayerInput _playerInput;
    private PlayerStateMachine _playerStateMachine;

    private WaitForSeconds _waitJumpInputBufferTime;
    [HideInInspector] public Rigidbody2D Rigidbody2D;
    
    [Header("Player info")]
    public PlayerStateData StateData;
    public PlayerParticleData ParticleData;
    [SerializeField]
    private GameObject _dashMask;
    private SpriteRenderer _dashMaskSprite;
    #region INPUT

    public Vector2 InputVector => _playerInput.Axis;
    public float XInput => _playerInput.Axis.x;
    public float YInput => _playerInput.Axis.y;
    public bool JumpInput => _playerInput.Jump;
    public bool StopJumpInput => _playerInput.StopJump;

    public bool ClimbInput => _playerInput.Climb;

    public bool DashInput => _playerInput.Dash;

    public bool CanDash
    {
        get => StateData.CanDash;
        set
        {
            StateData.CanDash = value;
            SetDashMaskAlpha(value);
        }
    }

    #endregion

    #region LifeMethod

    protected override void Awake()
    {
        base.Awake();
        this._playerInput = new PlayerInput();
        StateData = new PlayerStateData();
        _waitJumpInputBufferTime = new WaitForSeconds(StateData.JumpInputBufferTime);
        _playerStateMachine = new PlayerStateMachine(this);

        _playerStateMachine.ChangeState(PlayerStateEnum.Idle);
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (_dashMask != null)
        {
            _dashMaskSprite = _dashMask.GetComponent<SpriteRenderer>();
        }
        
    }

    private void OnEnable()
    {
        _playerInput.OnEnable();
        _playerStateMachine.ChangeState(PlayerStateEnum.Idle);
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
    }

    #endregion

    #region JUMP

    // ジャンプ入力バッファの設定
    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }

    private IEnumerator JumpInputBufferCoroutine()
    {
        StateData.HasJumpBuffer = true;
        Debug.Log(StateData.HasJumpBuffer);
        yield return _waitJumpInputBufferTime;
        StateData.HasJumpBuffer = false;
        Debug.Log(StateData.HasJumpBuffer);
    }

    #endregion

    #region DASH

    private void SetDashMaskAlpha(bool canDash)
    {
        if(_dashMaskSprite == null) return;
        
        Color color = _dashMaskSprite.color;
        if (canDash)
        {
            color.a = 0f;
            _dashMaskSprite.color = color;
        }
        else
        {
            color.a = 1f;
            _dashMaskSprite.color = color;
        }
        
    }

    #endregion
}