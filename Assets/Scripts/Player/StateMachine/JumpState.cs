
using UnityEngine;

public class JumpState : IPlayerState
{
    private bool isJumping;
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        Debug.Log("Entering Jump State");
        fsm.PlayerController.animationController.SetJump(true);

        fsm.CurrentStateType = PlayerState.Jump;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.animationController.SetJump(false);
        // throw new System.NotImplementedException();
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {

        if (InputManager.Instance.JumpTriggered)
        {
            isJumping = true;
            InputManager.Instance.JumpTriggered = false;
            if (fsm.PreviousState == fsm.IdleState || fsm.PreviousState == fsm.MoveState)
            {
                fsm.PlayerController.Jump();
            }
            else if (fsm.PreviousState == fsm.ClimbState)
            {
                fsm.PlayerController.JumpFromLadder();
            }

        }

        if (fsm.PreviousState == fsm.IdleState || fsm.PreviousState == fsm.MoveState)
        {
            fsm.PlayerController.Move(InputManager.Instance.MoveDirection);
        }


        fsm.PlayerController.animationController.SetDiveSpeed(fsm.PlayerController.Velocity.y);
        fsm.PlayerController.animationController.SetJump(isJumping);
        if (fsm.PlayerController.IsGrounded && fsm.PlayerController.Velocity.y <= 0)
        {
            Debug.Log(isJumping);
            if(InputManager.Instance.MoveTriggered)
            {
               fsm.SwitchState(fsm.MoveState);
            }
            else
            {
                fsm.SwitchState(fsm.IdleState);
            }
            isJumping = false;
        }

        // if (fsm.PlayerController.CanClimb)
        // {
        //     fsm.SwitchState(fsm.ClimbState);
        // }

        if (isJumping && fsm.PlayerController.CheckDiving())
        {
            fsm.SwitchState(fsm.DiveState);
        }

    }
}
