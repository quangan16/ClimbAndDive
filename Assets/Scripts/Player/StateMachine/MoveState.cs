
using UnityEngine;

public class MoveState : IPlayerState
{
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        fsm.CurrentStateType = PlayerState.Move;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {

    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.Move(InputManager.Instance.MoveDirection);
        if (fsm.PlayerController.IsGrounded && fsm.PlayerController.MoveVelocity.magnitude <=0)
        {
            fsm.SwitchState(fsm.IdleState);
        }

        if (fsm.PlayerController.IsGrounded && InputManager.Instance.JumpTriggered)
        {
            fsm.SwitchState(fsm.JumpState);
        }

        if (fsm.PlayerController.CanClimb)
        {
            fsm.SwitchState(fsm.ClimbState);
            // fsm.PlayerController.Climb();
        }




    }
}
