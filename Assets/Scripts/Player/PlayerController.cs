using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{

    public float maxMoveSpeed = 4f;
    public float rotationSmoothTime = 0.09f;

    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float smoothVelocity;

    public static event Action<float> OnPlayerMove;

    public float minimumDiveVelocity = 10;


    public bool IsGrounded =>  controller.isGrounded;


    public float jumpStrength = 1.5f;
    public float climbSpeed = 100f;

    [SerializeField] private bool canClimb = false;

    public Vector3 lastLadderSnapPoint = Vector3.zero;
    public Vector3 lastLadderContactNormal = Vector3.zero;
    public Vector3 lastLadderFaceContact = Vector3.zero;

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
        ApplyGravity();
    }

    public Vector3 moveVelocity;
    public Vector3 MoveVelocity => moveVelocity;
    public Vector3 slowDownVelocity;
    public void Move(Vector2 inputDir)
    {

        Vector2 input = inputDir;
        float inputMagnitude = inputDir.magnitude;
        // OnPlayerMove?.Invoke(inputMagnitude);
        if (inputMagnitude >= 0.1f)
        {
            // Camera-relative direction
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            RotateWithMoveDirection(targetAngle);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            moveVelocity = moveDir.normalized * inputMagnitude * maxMoveSpeed;
             // moveVelocity = Vector3.ClampMagnitude(moveVelocity, maxMoveSpeed);


        }
        else
        {
            if (moveVelocity.sqrMagnitude > 0.01f)
            {
                moveVelocity = Vector3.SmoothDamp(moveVelocity, Vector3.zero,ref  slowDownVelocity, 0.2f);
            }
            else
            {
                moveVelocity = Vector3.zero;
            }

            // moveVelocity = Vec tor3.Slerp(moveVelocity, Vector3.zero, Time.deltaTime);
        }

        print(moveVelocity.magnitude);
        OnPlayerMove?.Invoke(Mathf.Clamp01(moveVelocity.magnitude/maxMoveSpeed));
        controller.Move(moveVelocity  * Time.deltaTime);


    }

    public void RotateWithMoveDirection(float relativeAngle)
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, relativeAngle, ref smoothVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    public void ApplyGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (!controller.isGrounded)
        {
            return;
        }

        velocity.y = Mathf.Sqrt(jumpStrength * -2f * gravity);
        controller.Move(velocity * Time.deltaTime);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ladder")
        {
            canClimb = true;
            lastLadderSnapPoint = other.ClosestPointOnBounds(this.transform.position);
            lastLadderContactNormal = CalculateBoxColliderNormal(other, lastLadderSnapPoint);
        }
    }

    private Vector3 CalculateBoxColliderNormal(Collider collider, Vector3 contactPoint)
    {
        // Transform contact point to local space
        Vector3 localPoint = collider.transform.InverseTransformPoint(contactPoint);
        BoxCollider box = collider as BoxCollider;
        if (box == null) return Vector3.zero;

        // Get box dimensions
        Vector3 halfSize = box.size * 0.5f;
        Vector3 localCenter = box.center;

        // Calculate relative position from box center
        Vector3 relativePoint = localPoint - localCenter;

        // Determine which face is closest
        Vector3 normal = Vector3.zero;
        float minDistance = float.MaxValue;

        // Check each face (+x, -x, +y, -y, +z, -z)
        Vector3[] faceNormals = {
            Vector3.right, Vector3.left,
            Vector3.up, Vector3.down,
            Vector3.forward, Vector3.back
        };
        float[] faceDistances = {
            halfSize.x - Mathf.Abs(relativePoint.x), // +x, -x
            halfSize.x - Mathf.Abs(relativePoint.x),
            halfSize.y - Mathf.Abs(relativePoint.y), // +y, -y
            halfSize.y - Mathf.Abs(relativePoint.y),
            halfSize.z - Mathf.Abs(relativePoint.z), // +z, -z
            halfSize.z - Mathf.Abs(relativePoint.z)
        };

        for (int i = 0; i < faceNormals.Length; i++)
        {
            if (faceDistances[i] < minDistance)
            {
                minDistance = faceDistances[i];
                normal = faceNormals[i];
                lastLadderContactNormal = faceNormals[i];
                // Adjust for negative faces
                if (i % 2 == 1) normal = -normal;
            }
        }

        // Transform normal to world space
        return collider.transform.TransformDirection(normal).normalized;
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
        velocity.y = climbDirection * climbSpeed;
        SnapToLadder();
        controller.Move(velocity * Time.deltaTime);


        void SnapToLadder()
        {
            transform.position = lastLadderSnapPoint;
            transform.forward =  -lastLadderContactNormal;
        }
    }


    public bool CheckDiving()
    {
        return !controller.isGrounded && controller.velocity.magnitude > minimumDiveVelocity;
    }


}

