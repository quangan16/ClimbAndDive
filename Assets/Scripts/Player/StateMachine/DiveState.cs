
using UnityEngine;

public class DiveState : IPlayerState
{
    private float requiredHeight = 5.0f;
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        fsm.CurrentStateType = PlayerState.Dive;
        fsm.PlayerController.animationController.SetDive(true);

    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
         fsm.PlayerController.animationController.SetDive(false);

        // throw new System.NotImplementedException();
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        if (fsm.PlayerController.CurrentHeightToGround <= requiredHeight)
        {
            fsm.SwitchState(fsm.LandingState);
        }
    }
}
