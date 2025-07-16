

using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator playerAnimator;

    public void Start()
    {
        PlayerController.OnPlayerMove += SetMoveSpeed;


    }

    public void SetMoveSpeed(float moveSpeed)
    {

        playerAnimator.SetFloat("Speed", moveSpeed);
    }

    public void SetIdle(bool isIdling)
    {
        playerAnimator.SetBool("IsIdling", isIdling);
    }

    public void SetJump(bool isJump)
    {
        playerAnimator.SetBool("IsJumping", isJump);
    }

    public void SetClimb(bool isClimb)
    {
        print("111");
        playerAnimator.SetBool("IsClimbing", isClimb);
    }

    public void SetClimbSpeed(float climbSpeed)
    {
        playerAnimator.SetFloat("ClimbSpeed", climbSpeed);
    }

    public void SetDiveSpeed(float diveSpeed)
    {
        playerAnimator.SetFloat("DiveSpeed", diveSpeed);
    }

    public void SetDive(bool isDive)
    {
        playerAnimator.SetBool("IsDiving", isDive);
    }

    public void SetLanding(bool isLanding)
    {
        playerAnimator.SetBool("IsLanding", isLanding);
    }



    public void ResetAll()
    {
        SetIdle(false);
        SetJump(false);
        SetClimb(false);
    }

}
