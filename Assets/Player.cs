using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected Transform groundCheck; //地面チェック
    [SerializeField] protected float groundCheckDistance; //チェック距離
    [SerializeField] protected LayerMask whatIsGround; //レイヤー設定

    private Physics _physics;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _physics = new Physics(this.transform);
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.OnEnable();
    }

    private void OnDisable()
    {
        _playerInput.OnDisable();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
    }

    #region 判定

    /// <summary>
    /// 地面チェック
    /// </summary>
    /// <returns>true or false</returns>
    public bool IsGroundDetected() =>
        Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }

    #endregion
}