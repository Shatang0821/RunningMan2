using System.Collections;
using System.Collections.Generic;
using FrameWork.FSM;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected int stateBoolHash; // アニメーターのハッシュ値
    protected PlayerStateMachine playerStateMachine;
    protected Player player;
    protected static int currentFrame;
    public PlayerBaseState(string animBoolName,Player player,PlayerStateMachine playerStateMachine)
    {
        stateBoolHash = Animator.StringToHash(animBoolName); // アニメーターハッシュの初期化
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        Debug.Log(this.GetType().ToString());
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        currentFrame++;
    }
    
    /// <summary>
    /// フレーム単位でベクトル速度を制御するメソッド
    /// </summary>
    /// <param name="targetVelocity">目標速度ベクトル</param>
    /// <param name="totalFrames">最高速度に達するまでのフレーム数</param>
    protected void ChangeVelocity(Vector3 targetVelocity, int totalFrames)
    {
        if (currentFrame < totalFrames)
        {
            float lerpFactor = (float)currentFrame / totalFrames;
            Vector3 currentVelocity = Vector3.Lerp(player.Physics.Velocity, targetVelocity, lerpFactor);
            player.SetVelocity(currentVelocity);
        }
        else
        {
            player.SetVelocity(targetVelocity);
        }
    }
}
