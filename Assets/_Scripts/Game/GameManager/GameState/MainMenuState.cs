using FrameWork.FSM;
using FrameWork.Utils;
using UnityEngine.InputSystem;

public class MainMenuState : IState
{
    public MainMenuState()
    {
        
    }
    
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void LogicUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameManager.Instance.GamePlay();
        }
    }

    public void PhysicsUpdate()
    {
        
    }
}