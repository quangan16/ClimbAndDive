
public interface IPlayerState
{
    void Enter(PlayerFiniteStateMachine fsm = null);
    void Exit(PlayerFiniteStateMachine fsm = null);
    void Update(PlayerFiniteStateMachine fsm = null);
}
