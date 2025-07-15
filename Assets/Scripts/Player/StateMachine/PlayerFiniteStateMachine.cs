

using Unity.VisualScripting;
using UnityEngine;

public class PlayerFiniteStateMachine : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public PlayerController PlayerController => playerController;
    private IPlayerState _currentState;
    public PlayerState CurrentStateType;

    private IdleState idleState;
    public  IdleState IdleState => idleState;
    private MoveState moveState;
    public MoveState MoveState => moveState;
    private JumpState jumpState;
    public JumpState JumpState => jumpState;
    private DiveState diveState;

    public ClimbState climbState;
    public ClimbState ClimbState => climbState;
    public DiveState DiveState => diveState;
    public void Start()
    {
        idleState = new IdleState();
        moveState = new MoveState();
        jumpState = new JumpState();
        climbState = new ClimbState();
        diveState = new DiveState();
        _currentState = idleState;
        _currentState.Enter(this);
    }

    public void Update()
    {
        _currentState.Update(this);

    }



    public void SwitchState(IPlayerState newState)
    {
        if (newState == null || newState == _currentState) return;
        _currentState.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }
}

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Climb,
    Dive,

}
