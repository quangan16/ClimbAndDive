
using System;
using UnityEngine;

public class ClimbState : IPlayerState
{
    public event Action OnPlayerStartClimb;
    public event Action OnPlayerClimbing;
    public event Action OnPlayerEndClimb;
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        Debug.Log("Enter climbing state");
        fsm.CurrentStateType = PlayerState.Climb;
        fsm.PlayerController.IsClimbing = true;
        fsm.PlayerController.animationController.SetClimb(true);
        GameManager.Instance.cameraController.CameraOffsetTarget = Vector3.back * 5;
        OnPlayerStartClimb?.Invoke();
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.CanClimb = false;
        fsm.PlayerController.IsClimbing = false;
        fsm.PlayerController.animationController.SetClimb(false);
        GameManager.Instance.cameraController.CameraOffsetTarget = Vector3.zero;
        OnPlayerEndClimb?.Invoke();

    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        // if (!fsm.PlayerController.CanClimb) return;
        fsm.PlayerController.Climb();
        OnPlayerClimbing?.Invoke();
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
        OnPlayerClimbing = null;
        OnPlayerEndClimb = null;
    }


}
