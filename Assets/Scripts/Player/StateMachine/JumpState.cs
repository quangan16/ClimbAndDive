
using UnityEngine;

public class JumpState : IPlayerState
{
    private bool isJumping;
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {

        fsm.CurrentStateType = PlayerState.Jump;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        // throw new System.NotImplementedException();
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {

        if (InputManager.Instance.JumpTriggered)
        {
            isJumping = true;
            Debug.Log("Jump triggered1");
            InputManager.Instance.JumpTriggered = false;
            fsm.PlayerController.Jump();
        }
        fsm.PlayerController.Move(InputManager.Instance.MoveDirection);
        if (fsm.PlayerController.IsGrounded && !isJumping)
        {
            Debug.Log("Jump triggered2");
            if(InputManager.Instance.MoveTriggered)
            {
               fsm.SwitchState(fsm.MoveState);
            }
            else
            {
                fsm.SwitchState(fsm.IdleState);
            }
        }

        if (fsm.PlayerController.CanClimb)
        {
            fsm.SwitchState(fsm.ClimbState);
        }

        if (!fsm.PlayerController.IsGrounded && isJumping && fsm.PlayerController.CheckDiving())
        {
            fsm.SwitchState(fsm.DiveState);
        }
        isJumping = false;
    }
}
