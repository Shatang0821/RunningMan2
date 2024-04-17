using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Physics
{
    private Transform _transform;   //移動する位置
    public const float Gravity = 9f; //重力
    public Vector2 Velocity;        //速度
    private float _fallSpeed;
    private const float MaxFallSpeed = 50;
    public Physics(Transform transform)
    { 
        _transform = transform;
        Velocity = Vector2.zero;
    }
    
    /// <summary>
    /// 物理更新処理
    /// </summary>
    public void HorizontalUpdate()
    {
        var xVelocity = Velocity.x * Time.fixedDeltaTime;
        //Debug.Log(position);
        _transform.position += new Vector3(xVelocity,0,0);
    }

    public void VerticalUpdate()
    {
        // まず、_fallSpeedを更新
        _fallSpeed -= Gravity * Time.fixedDeltaTime;
        _fallSpeed = Mathf.Clamp(_fallSpeed,-MaxFallSpeed, 0);

        
        Velocity.y = (Velocity.y + _fallSpeed) * Time.fixedDeltaTime;
        Debug.Log(Velocity.y);
        _transform.position += new Vector3(0, Velocity.y, 0);
    }
    
    public void AddForce(Vector2 forceVector)
    {
        Velocity += forceVector;
    }
}