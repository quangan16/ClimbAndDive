
using UnityEngine;

public class IdleState : IPlayerState
{
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        Debug.Log("IdleState");;
        fsm.PlayerController.animationController.SetIdle(true);
        fsm.CurrentStateType = PlayerState.Idle;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.animationController.SetIdle(false);
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
