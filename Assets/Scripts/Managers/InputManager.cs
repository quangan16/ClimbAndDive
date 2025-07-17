using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] private Vector2 moveDirection;
    public Vector2 MoveDirection => moveDirection;
    [SerializeField] private Vector2 cameraDirection;
    public Vector2 CameraDirection {
        get { return cameraDirection; }

        set
        {
            cameraDirection = value;
        }
    }



    public bool jumpTriggered = false;

    public bool MoveTriggered => moveDirection.magnitude > 0;
    public bool JumpTriggered
    {
        get { return jumpTriggered; }
        set { jumpTriggered = value; }
    }

    public static  Action OnJumpButtonClicked;

    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        // CameraSwipeArea.OnSwipe += HandleSwipeCameraAngleInput;
        CameraSwipeArea.OnEndSwipe += ResetCameraDirection;
    }

    public void HandleJoystickMovementInput(Vector2 joystickDir)
    {
        moveDirection = joystickDir;
    }

    public void HandleSwipeCameraAngleInput(Vector2 swipeDir)
    {
        cameraDirection = swipeDir;
        // cameraDirection = Vector3.zero;
    }

    public void ResetCameraDirection()
    {
        print("Reset camera direction");
        cameraDirection = Vector2.zero;
    }

    public void HandleJumpButtonInput()
    {
        jumpTriggered = true;
    }


    public enum InputType
    {
        Joystick,
    }

}
