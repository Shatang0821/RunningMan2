using System.Collections;
using System.Collections.Generic;
using FrameWork.FSM;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected int stateBoolHash; // アニメーターのハッシュ値
    protected PlayerStateMachine playerStateMachine;
    protected Player player;
    
    //　状態のフレーム  
    protected int currentFrame
    {
        get => player.StateData.CurrentFrame;
        set => player.StateData.CurrentFrame = value;
    }           
    // 状態のタイマー
    protected float stateTimer
    {
        get => player.StateData.StateTimer;
        set => player.StateData.StateTimer = value;
    }
    
    public PlayerBaseState(string animBoolName,Player player,PlayerStateMachine playerStateMachine)
    {
        stateBoolHash = Animator.StringToHash(animBoolName); // アニメーターハッシュの初期化
        this.player = player;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(stateBoolHash,true);
        Debug.Log(this.GetType().ToString());
        currentFrame = 0;
    }

    public virtual void Exit()
    {
        player.anim.SetBool(stateBoolHash,false);
    }
    
    public virtual void LogicUpdate()
    {
        /*
         * 遷移条件は優先度高い順に書く
         */
        stateTimer -= Time.deltaTime;             // 状態タイマーの更新
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
            Vector3 currentVelocity = Vector3.Lerp(player.Rigidbody2D.velocity, targetVelocity, lerpFactor);
            player.SetVelocity(currentVelocity);
        }
        else
        {
            player.SetVelocity(targetVelocity);
        }
    }
}
