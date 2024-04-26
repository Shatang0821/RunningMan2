using FrameWork.FSM;
public enum PlayerStateEnum
{
   Idle,
   Run,
   Jump,
   Fall,
   CoyoteTime
}

public class PlayerStateMachine : StateMachine<PlayerStateEnum>
{
   public PlayerStateMachine(Player player)
   {
      RegisterState(PlayerStateEnum.Idle,new PlayerIdleState(PlayerStateEnum.Idle.ToString(),player,this));
      RegisterState(PlayerStateEnum.Run,new PlayerRunState(PlayerStateEnum.Run.ToString(),player,this));
      RegisterState(PlayerStateEnum.Jump,new PlayerJumpState("InAir",player,this));
      RegisterState(PlayerStateEnum.Fall,new PlayerFallState("InAir",player,this));
      RegisterState(PlayerStateEnum.CoyoteTime,new PlayerCoyoteState(PlayerStateEnum.Run.ToString(),player,this));
   }
}
