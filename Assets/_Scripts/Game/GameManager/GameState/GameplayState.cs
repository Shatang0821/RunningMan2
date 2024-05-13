using FrameWork.FSM;
using UnityEngine;

public class GameplayState : IState
{
    private PlayerController _playerController;
    private StageManager _stageManager;

    public GameplayState()
    {
        _playerController = new PlayerController();
        _stageManager = new StageManager();
    }

    public void Enter()
    {
        _stageManager.GamePlayerEnter();
        _playerController.GamePlayerEnter(_stageManager.GetCurrentMapData().PlayerSpawnPos);
        CameraController.Instance.GamePlayerEnter(_playerController.GetPlayer());
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