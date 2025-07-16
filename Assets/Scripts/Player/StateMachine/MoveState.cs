
using UnityEngine;

public class MoveState : IPlayerState
{
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        Debug.Log("Move");
        fsm.CurrentStateType = PlayerState.Move;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.animationController.SetMoveSpeed(0);
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.Move(InputManager.Instance.MoveDirection);

        if (fsm.PlayerController.MoveVelocity.magnitude <0.01)
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
