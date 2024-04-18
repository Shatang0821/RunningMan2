using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Physics
{
    private Transform _transform; //移動する位置

    private float _xAccelerationRate = 6.0f; // 加速度（単位：m/s²）
    private float _xDecelerationRate = 12.0f; // 減速度（単位：m/s²）
    private float _xMaxSpeed = 3.0f;
    
    private float _yAccelerationRate = 6.0f; // 加速度（単位：m/s²）
    private float _yDecelerationRate = 12.0f; // 減速度（単位：m/s²）
    private float _yMaxSpeed = 3.0f;

    private Vector3 velocity; // 現在の速度
    private Vector3 position; // 現在の位置

    public Physics(Transform transform)
    {
        _transform = transform;
    }
    

    public void UpdateVelocity(float input)
    {
        if (input != 0)
        {
            //加速度を積分すると速度になる
            velocity.x += input * _xAccelerationRate * Time.fixedDeltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -_xMaxSpeed, _xMaxSpeed);
        }
        else if (velocity.x != 0)
        {
            float deceleration = Mathf.Sign(velocity.x) * _xDecelerationRate * Time.fixedDeltaTime;
            if (Mathf.Abs(deceleration) > Mathf.Abs(velocity.x))
                velocity.x = 0;
            else
                velocity.x -= deceleration;
        }
        // 重力の追加
        
        UpdatePosition();
    }
    
    public void UpdatePosition()
    {
        // 位置の更新（積分）
        position += velocity * Time.fixedDeltaTime;
        ApplyPosition();
    }
    
    private void ApplyPosition()
    {
        // Transformの位置を更新
        _transform.position = position;
    }
}