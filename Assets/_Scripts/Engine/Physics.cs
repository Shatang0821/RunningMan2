using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[Serializable]
public class Physics
{
    private Transform _transform; //移動する位置

    public Vector3 Velocity; // 現在の速度
    //public float Gravity = -9.81f;
    private Vector3 position; // 現在の位置

    public Physics(Transform transform)
    {
        _transform = transform;
    }
    
    public void UpdatePosition()
    {
        // 位置の更新（積分）
        //Velocity.y += Gravity * Time.fixedDeltaTime;
        position += Velocity * Time.fixedDeltaTime;
        ApplyPosition();
    }
    
    private void ApplyPosition()
    {
        // Transformの位置を更新
        _transform.position = position;
    }
    
}