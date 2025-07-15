

using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator playerAnimator;

    public void Start()
    {
        PlayerController.OnPlayerMove += SetSpeed;
    }

    public void SetSpeed(float speed)
    {
        print("sp " + speed);
        playerAnimator.SetFloat("Speed", speed);
    }

    public void SetIdle()
    {
        playerAnimator.SetTrigger("idle");
    }
}
