using FrameWork.EventCenter;
using FrameWork.Factories;
using FrameWork.FSM;
using FrameWork.Utils;
using UnityEngine;

public class GameplayState : IState
{
    private PlayerController _playerController = new PlayerController();

    public void Enter()
    {
        _playerController.GamePlayerEnter();
    }

    public void Exit()
    {
        _playerController.GamePlayerExit();
    }

    public void LogicUpdate()
    {
        
    }

    public void PhysicsUpdate()
    {
        
    }
}