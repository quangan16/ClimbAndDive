using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MovementJoystickArea : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public static event Func<PointerEventData, Vector2> OnJoystickInteracted;
    public static event Action OnJoystickOff;

    public Vector2 direction;

    public MovementJoystickController movementJoystick;

    public bool isTouching;


    public void Update()
    {


    }
    public void OnDrag(PointerEventData eventData)
    {
        print("OnDrag");
        OnJoystickInteracted?.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        movementJoystick.ResetHandlePosition();
        movementJoystick.gameObject.SetActive(true);
        movementJoystick.transform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
        movementJoystick.gameObject.SetActive(false);
        movementJoystick.ResetHandlePosition();
        OnJoystickOff?.Invoke();
    }
}
