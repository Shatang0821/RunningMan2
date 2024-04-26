using System;
using FrameWork.Utils;
using Unity.VisualScripting;
using UnityEngine;


[Serializable]
public class Player : Entity
{
    private PlayerInput _playerInput;
    private PlayerStateMachine _playerStateMachine;
    [HideInInspector]
    public Rigidbody2D Rigidbody2D;
    public float XInput => _playerInput.Axis.x;
    public float YInput => _playerInput.Axis.y;
    public bool IsJump => _playerInput.Jump;
    public bool IsStopJump => _playerInput.StopJump;

    public PlayerStateData StateData;
    #region LifeMethod

    protected override void Awake()
    {
        base.Awake();
        this._playerInput = new PlayerInput();
        StateData = ScriptableObject.CreateInstance<PlayerStateData>();
        _playerStateMachine = new PlayerStateMachine(this);
        
        _playerStateMachine.ChangeState(PlayerStateEnum.Idle);
        Rigidbody2D = GetComponent<Rigidbody2D>();
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
}