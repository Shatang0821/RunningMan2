﻿using System;
using FrameWork.Factories;
using FrameWork.Utils;
using UnityEngine;

public enum GameStateEnum
{
    MainMenu,
    GamePlay,
    GameOver
}
public class GameManager : PersistentUnitySingleton<GameManager>
{
    private GameStateMachine stateMachine;
    
    protected override void Awake()
    {
        base.Awake();
        ManagerFactory.Instance.CreateManager<MapManager>();
        stateMachine = new GameStateMachine();
        GamePlay();
    }

    private void Update()
    {
        stateMachine.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    //状態遷移ロジック
    private void GamePlay()
    {
        stateMachine.ChangeState(GameStateEnum.GamePlay);
    }

    private void GameOver()
    {
        stateMachine.ChangeState(GameStateEnum.GameOver);
    }

    private void MainMenu()
    {
        stateMachine.ChangeState(GameStateEnum.MainMenu);
    }
}