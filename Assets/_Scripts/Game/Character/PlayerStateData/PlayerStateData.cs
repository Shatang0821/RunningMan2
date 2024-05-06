public class PlayerStateData
{
    public int CurrentFrame;             //　状態のフレーム  
    public float StateTimer;             // 状態のタイマー
    public float Gravity = 0;            // 重力
    public bool HasJumpBuffer = false;
    public float JumpInputBufferTime = 0.1f;
    
}