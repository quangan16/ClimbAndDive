
using System;
using UnityEngine;

public class ClimbState : IPlayerState
{
    public event Action OnPlayerStartClimb;
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        Debug.Log("Enter climbing state");
        fsm.CurrentStateType = PlayerState.Climb;
        fsm.PlayerController.IsClimbing = true;
        fsm.PlayerController.animationController.SetClimb(true);
        GameManager.Instance.cameraController.CameraOffsetTarget = Vector3.back * 5;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.CanClimb = false;
        fsm.PlayerController.IsClimbing = false;
        fsm.PlayerController.animationController.SetClimb(false);
        GameManager.Instance.cameraController.CameraOffsetTarget = Vector3.zero;
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        // if (!fsm.PlayerController.CanClimb) return;
        fsm.PlayerController.Climb();
        if (fsm.PlayerController.IsGrounded && InputManager.Instance.MoveDirection.y < 0)
        {
            fsm.SwitchState(fsm.MoveState);
        }

        if ( InputManager.Instance.JumpTriggered)
        {
            fsm.SwitchState(fsm.JumpState);
        }
    }


    public void OnDestroy()
    {
        OnPlayerStartClimb = null;
    }


}
