using System.Collections.Generic;
using FrameWork.FSM;

public class GameStateMachine : StateMachine<GameStateEnum>
{
    public GameStateMachine()
    {
        RegisterState(GameStateEnum.MainMenu,new MainMenuState());
        RegisterState(GameStateEnum.GamePlay,new GameplayState());
        RegisterState(GameStateEnum.GameOver,new GameOverState());
    }
}