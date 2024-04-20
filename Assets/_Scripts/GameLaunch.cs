using System;
using FrameWork.Factories;
using FrameWork.Pool;
using FrameWork.Resource;
using FrameWork.UI;
using FrameWork.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class GameLaunch : UnitySingleton<GameLaunch>
{
    protected override void Awake()
    {
        base.Awake();
        this.InitFramework();
        this.InitManagers();

    }

    /// <summary>
    /// フレームワークを初期化
    /// </summary>
    private void InitFramework()
    {
        ManagerFactory.Instance.CreateManager<UIManager>();
    }

    /// <summary>
    /// ゲームロジックに入る
    /// </summary>
    private void InitManagers()
    {
        ManagerFactory.Instance.CreateManager<PoolManager>();
        ManagerFactory.Instance.CreateManager<GameManager>();
    }
    
}