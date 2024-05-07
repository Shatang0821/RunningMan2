using FrameWork.FSM;

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