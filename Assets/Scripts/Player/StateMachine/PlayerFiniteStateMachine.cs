

using Unity.VisualScripting;
using UnityEngine;

public class PlayerFiniteStateMachine : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    public PlayerController PlayerController => playerController;
    private IPlayerState _previousState;
    public IPlayerState PreviousState => _previousState;
    private IPlayerState _currentState;
    public PlayerState CurrentStateType;

    private IdleState _idleState;
    public  IdleState IdleState => _idleState;
    private MoveState _moveState;
    public MoveState MoveState => _moveState;
    private JumpState _jumpState;
    public JumpState JumpState => _jumpState;
    private DiveState _diveState;

    private ClimbState _climbState;
    public ClimbState ClimbState => _climbState;
    public DiveState DiveState => _diveState;
    private LandingState _landingState;
    public LandingState LandingState => _landingState;

    public void Start()
    {
        _idleState = new IdleState();
        _moveState = new MoveState();
        _jumpState = new JumpState();
        _climbState = new ClimbState();
        _diveState = new DiveState();
        _landingState = new LandingState();
        _currentState = _idleState;
        _currentState.Enter(this);
        _previousState = null;
    }

    public void Update()
    {
        _currentState.Update(this);
    }



    public void SwitchState(IPlayerState newState)
    {
        if (newState == null || newState == _currentState) return;
        _currentState.Exit(this);
        _previousState = _currentState;
        _currentState = newState;
        _currentState.Enter(this);
    }

    public void OnDestroy()
    {
        _climbState.OnDestroy();
    }
}

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Climb,
    Dive,
    Landing

}
