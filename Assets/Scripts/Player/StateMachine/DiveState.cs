
public class DiveState : IPlayerState
{
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        fsm.CurrentStateType = PlayerState.Dive;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        // throw new System.NotImplementedException();
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        fsm.CurrentStateType = PlayerState.Dive;
    }
}
