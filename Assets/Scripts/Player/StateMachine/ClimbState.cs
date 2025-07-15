
public class ClimbState : IPlayerState
{
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        fsm.CurrentStateType =  PlayerState.Climb;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.CanClimb = false;
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        if (!fsm.PlayerController.CanClimb) return;
        fsm.PlayerController.Climb();
        if (fsm.PlayerController.IsGrounded && InputManager.Instance.MoveDirection.y < 0)
        {
            fsm.SwitchState(fsm.MoveState);
        }
    }
}
