using FrameWork.FSM;
public enum PlayerStateEnum
{
   Idle,
   Run,
   Jump,
   Fall
}

public class PlayerStateMachine : StateMachine<PlayerStateEnum>
{
   public PlayerStateMachine(Player player)
   {
      RegisterState(PlayerStateEnum.Idle,new PlayerIdleState(PlayerStateEnum.Idle.ToString(),player,this));
      RegisterState(PlayerStateEnum.Run,new PlayerRunState(PlayerStateEnum.Run.ToString(),player,this));
      RegisterState(PlayerStateEnum.Jump,new PlayerJumpState(PlayerStateEnum.Jump.ToString(),player,this));
      RegisterState(PlayerStateEnum.Fall,new PlayerFallState(PlayerStateEnum.Fall.ToString(),player,this));
   }
}
