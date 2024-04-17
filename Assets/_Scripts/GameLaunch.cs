using System;
using FrameWork.Factories;
using FrameWork.Resource;
using FrameWork.UI;
using FrameWork.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class GameLaunch : UnitySingleton<GameLaunch>
{
    public Transform Transform;
    private Physics _physics;
    private PlayerInput _playerInput = new PlayerInput();
    protected override void Awake()
    {
        base.Awake();
        this.InitFramework();
        this.InitGameLogic();
        _physics = new Physics(Transform);
    }
    
    /// <summary>
    /// フレームワークを初期化
    /// </summary>
    private void InitFramework()
    {
        _playerInput.Init();
        ManagerFactory.Instance.CreateManager<ResManager>();
        ManagerFactory.Instance.CreateManager<UIManager>();
    }

    /// <summary>
    /// ゲームロジックに入る
    /// </summary>
    private void InitGameLogic()
    {
        this.gameObject.AddComponent<GameApp>();
        GameApp.Instance.InitGame();
    }

    private void OnEnable()
    {
        _playerInput.OnEnable();
    }

    private void OnDisable()
    {
        _playerInput.OnDisable();
    }

    private void Update()
    {
        _physics.Velocity.x = _playerInput.Axis.x;
        if (_playerInput.Jump)
        {
            _physics.AddForce(new Vector2(0,10));
        }
    }

    private void FixedUpdate()
    {
        _physics.HorizontalUpdate();
        _physics.VerticalUpdate();
    }
}
