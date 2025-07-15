
public class IdleState : IPlayerState
{
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        fsm.CurrentStateType = PlayerState.Idle;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {

    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        if (InputManager.Instance.MoveTriggered)
        {
            fsm.SwitchState(fsm.MoveState);
        }

        if (InputManager.Instance.JumpTriggered)
        {
            fsm.SwitchState(fsm.JumpState);
        }
    }
}
