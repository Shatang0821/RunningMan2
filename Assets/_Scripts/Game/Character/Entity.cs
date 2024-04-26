using System.Collections;
using System.Collections.Generic;
using FrameWork.Pool;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public Collider2D cd { get; private set; }

    #endregion

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;         //地面チェック
    [SerializeField] protected float groundCheckDistance;     //チェック距離
    [SerializeField] protected Transform wallCheck;           //壁チェック
    [SerializeField] protected float wallCheckDistance;       //チェック距離
    [SerializeField] protected LayerMask whatIsGround;        //レイヤー設定



    /// <summary>
    /// 向いている方向
    /// </summary>
    public int facingDir { get; private set; } = 1;

    protected bool facingRight = true;

    [Header("==== DEATH ====")]
    public GameObject DeathVFX; // 死亡時のエフェクト
    protected bool isDeaded; // 死亡状態を表すフラグ

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        if(anim == null)
        {
            Debug.Log(this.name + "anim is null");
        }
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    #region Velocity
    public void ZeroVelocity() => rb.velocity = Vector2.zero;

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    /// <summary>
    /// X速度設定
    /// </summary>
    /// <param name="velocityX">X速度</param>
    public void SetVelocityX(float velocityX)
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    /// <summary>
    /// Y速度設定
    /// </summary>
    /// <param name="velocityY">Y速度</param>
    public void SetVelocityY(float velocityY)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }

    public void SetGravity(float value)
    {
        rb.gravityScale = value;
    }
    #endregion

    #region Collision
    /// <summary>
    /// 地面チェック
    /// </summary>
    /// <returns>true or false</returns>
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    /// <summary>
    /// 壁チェック
    /// </summary>
    /// <returns>true or false</returns>
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        
    }
    #endregion

    #region Flip
    /// <summary>
    /// プレイヤー反転関数
    /// </summary>
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        //y軸中心に180回転
        transform.Rotate(0, 180, 0);
    }

    /// <summary>
    /// 反転判断する関数
    /// </summary>
    /// <param name="_x"></param>
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();

    }
    #endregion

    #region Die
    public virtual void Die()
    {
        PoolManager.Release(DeathVFX, transform.position);
        this.gameObject.SetActive(false);
    }
    #endregion
}
