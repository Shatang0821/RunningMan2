using UnityEngine;


public class PlayerStateData : ScriptableObject
{
    public int CurrentFrame;             //　状態のフレーム  
    public float StateTimer;             // 状態のタイマー
    public float Gravity = 0;                // 重力
    public float CotoyeTime;             // コヨテタイム
}