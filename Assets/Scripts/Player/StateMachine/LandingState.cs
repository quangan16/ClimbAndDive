
using UnityEngine;

public class LandingState : IPlayerState
{

    private float maxLandingTime = 0.7f;
    private float trackLandingTime;
    public void Enter(PlayerFiniteStateMachine fsm = null)
    {
        trackLandingTime = 0.0f;
        Debug.Log("Enter Landing state");;
        fsm.PlayerController.animationController.SetLanding(true);
        fsm.CurrentStateType = PlayerState.Landing;
    }

    public void Exit(PlayerFiniteStateMachine fsm = null)
    {
        fsm.PlayerController.animationController.SetLanding(false);
        EffectManager.Instance.TriggerResourcesEffect(Vector3.zero);
    }

    public void Update(PlayerFiniteStateMachine fsm = null)
    {
        trackLandingTime += Time.deltaTime;

        if(trackLandingTime > maxLandingTime) fsm.SwitchState(fsm.IdleState);
    }
}
