using FrameWork.FSM;
public enum PlayerStateEnum
{
   Idle,
   Run,
   Jump,
   Fall,
   Land,
   CoyoteTime,
   Climb,
   ClimbLeap,
   WallSlide,
   WallJump
}

public class PlayerStateMachine : StateMachine<PlayerStateEnum>
{
   public PlayerStateMachine(Player player)
   {
      //GroundState
      RegisterState(PlayerStateEnum.Idle,new PlayerIdleState(PlayerStateEnum.Idle.ToString(),player,this));
      RegisterState(PlayerStateEnum.Run,new PlayerRunState(PlayerStateEnum.Run.ToString(),player,this));
      RegisterState(PlayerStateEnum.Land,new PlayerLandState(PlayerStateEnum.Idle.ToString(),player,this));
      RegisterState(PlayerStateEnum.CoyoteTime,new PlayerCoyoteState(PlayerStateEnum.Run.ToString(),player,this));
      //AirState
      RegisterState(PlayerStateEnum.Jump,new PlayerJumpState("InAir",player,this));
      RegisterState(PlayerStateEnum.Fall,new PlayerFallState("InAir",player,this));
      RegisterState(PlayerStateEnum.ClimbLeap,new PlayerClimbLeapState("InAir",player,this));
      RegisterState(PlayerStateEnum.WallJump,new PlayerWallJumpState("InAir",player,this));
      //WallState
      RegisterState(PlayerStateEnum.Climb,new PlayerClimbState("Climb",player,this));
      RegisterState(PlayerStateEnum.WallSlide,new PlayerWallSlideState("WallSlide",player,this));
   }
}
