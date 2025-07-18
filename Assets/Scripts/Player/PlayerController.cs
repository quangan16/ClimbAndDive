using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour, IClimber
{
    public PlayerFiniteStateMachine fsm;
    public PlayerAnimationController animationController;
    public float maxMoveSpeed = 4f;
    public float rotationSmoothTime = 0.09f;

    private CharacterController controller;
    private Vector3 velocity;
    public Vector3 Velocity => velocity;
    private float gravity = -9.81f;
    public float GravityMultiplier = 1.5f;
    private float smoothVelocity;
    private float heightRequireToDive = 7.0f;
    public float HeightRequireToDive => heightRequireToDive;

    [SerializeField] private Transform refPoint;

    public float CurrentHeightToGround => (transform.position - refPoint.transform.position).magnitude;

    public static event Action<float> OnPlayerMove;




    public float MinimumDiveVelocity { get; set; } = -8;




    public LayerMask groundLayer;
    public float skinWidth = 1f;
    public bool IsGrounded =>   Physics.Raycast(transform.position, Vector3.down, 0.1f + skinWidth, groundLayer);


    public float jumpStrength = 1.5f;
    public float ladderJumpStrength = 1f;
    public float ClimbSpeed { get; set; }= 2f;


    [SerializeField] private bool canClimb = false;
    [SerializeField] private bool isClimbing = false;
    public float ClimbProgress()
    {
        if (fsm.CurrentStateType != PlayerState.Climb) return 0f;
        float progress = Mathf.Clamp01(CurrentHeightToGround / GameManager.Instance.ladder.TotalHeight);
        return progress;
    }

    public bool IsClimbing
    {
        get => isClimbing;
        set => isClimbing = value;
    }

    public Vector3 LastLadderSnapPoint { get; set; }= Vector3.zero;
    public Vector3 LastLadderContactNormal { get; set; } = Vector3.zero;
    public Vector3 LastLadderFaceContact { get; set; }= Vector3.zero;

    public bool CanClimb
    {
        get { return canClimb; }
        set { canClimb = value; }
    }
    [SerializeField] private Camera cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // InputManager.OnJumpButtonClicked += Jump;
        // InputManager.Instance.OnMovementInputChanged += Move;
    }

    void Update()
    {
        // print(IsGrounded);
        // print(CurrentHeightToGround);
        ApplyGravity();


    }

    public Vector3 moveVelocity;
    public Vector3 MoveVelocity => moveVelocity;
    public Vector3 slowDownVelocity;


    public void Move(Vector2 inputDir)
    {

        Vector2 input = inputDir;
        float inputMagnitude = inputDir.magnitude;
        if (inputMagnitude >= 0.1f)
        {
            // Camera-relative direction
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            RotateWithMoveDirection(targetAngle);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            // print(moveDir);
            moveVelocity = moveDir.normalized * inputMagnitude * maxMoveSpeed;
             // moveVelocity = Vector3.ClampMagnitude(moveVelocity, maxMoveSpeed)
        }
        else
        {
            if (moveVelocity.sqrMagnitude > 0.01f)
            {
                moveVelocity = Vector3.SmoothDamp(moveVelocity, new Vector3(0, moveVelocity.y, 0),ref  slowDownVelocity, 0.2f);
            }
            else
            {
                moveVelocity = new Vector3(0, moveVelocity.y, 0);
            }
        }

        if (fsm.CurrentStateType == PlayerState.Move)
        {
            OnPlayerMove?.Invoke(Mathf.Clamp01(moveVelocity.magnitude/maxMoveSpeed));
        }
        controller.Move(moveVelocity  * Time.deltaTime);
        // if (fsm.CurrentStateType == PlayerState.Move)
        // {
        //     OnPlayerMove?.Invoke(Mathf.Clamp01(moveVelocity.magnitude/maxMoveSpeed));
        // }

        // controller.Move(moveVelocity  * Time.deltaTime);


    }





    public void RotateWithMoveDirection(float relativeAngle)
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, relativeAngle, ref smoothVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void ApplyGravity()
    {
        if (IsClimbing) return;
        if (IsGrounded && velocity.y < 0)
            velocity.y = -2f;
        velocity.y += gravity * GravityMultiplier * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (!IsGrounded)
        {
            return;
        }

        velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        controller.Move(velocity * Time.deltaTime);

    }

    public void JumpFromLadder()
    {

        velocity.y = Mathf.Sqrt(ladderJumpStrength * -2f * gravity);
        controller.Move(Quaternion.Euler(0, 180, 0) * transform.forward +  velocity *  Time.deltaTime);
        StartCoroutine(RotateOverTime(new Vector3(0, 180, 0), 0.12f));
    }

    IEnumerator RotateOverTime(Vector3 targetAngle, float duration)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(targetAngle);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localRotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ladder")
        {
            if (canClimb == false)
            {
                canClimb = true;
            }

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ladder")
        {
            LastLadderSnapPoint = other.ClosestPointOnBounds(this.transform.position);
             LastLadderContactNormal =  CalculateBoxColliderNormal(other, LastLadderSnapPoint);
        }
    }

    private Vector3 CalculateBoxColliderNormal(Collider collider, Vector3 contactPoint)
    {
        BoxCollider box = collider as BoxCollider;
        if (box == null) return Vector3.zero;

        // Convert the contact point into the local space of the collider
        Vector3 localPoint = collider.transform.InverseTransformPoint(contactPoint);

        // Get box center and half size
        Vector3 localCenter = box.center;
        Vector3 halfSize = box.size * 0.5f;

        // Compute point relative to box center
        Vector3 relative = localPoint - localCenter;

        // Find the closest face
        float dx = halfSize.x - Mathf.Abs(relative.x);
        float dy = halfSize.y - Mathf.Abs(relative.y);
        float dz = halfSize.z - Mathf.Abs(relative.z);

        Vector3 localNormal;

        if (dx < dy && dx < dz)
        {
            localNormal = new Vector3(Mathf.Sign(relative.x), 0, 0);
        }
        else if (dy < dz)
        {
            localNormal = new Vector3(0, Mathf.Sign(relative.y), 0);
        }
        else
        {
            localNormal = new Vector3(0, 0, Mathf.Sign(relative.z));
        }

        // Transform normal back to world space
        return collider.transform.TransformDirection(localNormal);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladder")
        {
            canClimb = false;
        }
    }

    public void Climb()
    {
        if (!canClimb) return;
        var climbDirection = InputManager.Instance.MoveDirection.y;
        velocity.y = climbDirection * ClimbSpeed;
        animationController.SetClimbSpeed(velocity.y);
        SnapToLadder();
        controller.Move(velocity * Time.deltaTime);


        void SnapToLadder()
        {
            transform.position = LastLadderSnapPoint;
            transform.forward = -LastLadderContactNormal;
        }
    }


    public bool CheckDiving()
    {
        return controller.velocity.y < MinimumDiveVelocity && CurrentHeightToGround >= heightRequireToDive;
    }


}

