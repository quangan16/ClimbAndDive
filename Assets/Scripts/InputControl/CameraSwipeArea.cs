

using System;
using UnityEngine;using UnityEngine.EventSystems;

public class CameraSwipeArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler, IBeginDragHandler, IEndDragHandler
{

    public float swipeThreshold;

    [SerializeField] private bool isPressing;
    public static event Action<Vector2> OnSwipe;
    public static event Action OnEndSwipe;
    Vector3 prevPosition =  Vector3.zero;
    public bool isDragging;

    public CameraController cameraController;

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging) return;

        var swipeDirection =eventData.delta;

        if (swipeDirection.magnitude > swipeThreshold)
        {
            print("Dragging");
            cameraController.UpdatePosAndRotatetion(swipeDirection);
            // OnSwipe?.Invoke(swipeDirection);
            print(eventData.delta);

        }



        prevPosition = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        // throw new
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        // swipeDirection = Vector2.zero;
        OnEndSwipe?.Invoke();
    }



    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
        print("OnPointerUp");
        OnEndSwipe?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
        prevPosition = eventData.position;
        print("OnPointerDown");
    }
    // Vector2 prevTouchPos =  Vector2.zero;
    // public void Update()
    // {
    //     if (Input.touchCount <= 0) return;
    //
    //     Touch touch =  Input.GetTouch(0);
    //     // var prevTouch = touch.position;
    //
    //
    //     if (touch.phase == TouchPhase.Began)
    //     {
    //         if (!RectTransformUtility.RectangleContainsScreenPoint(this.GetComponent<RectTransform>(), touch.position))
    //         {
    //             return;
    //         }
    //         prevTouchPos = touch.position;
    //     }
    //     if (touch.phase == TouchPhase.Moved)
    //     {
    //         if (!RectTransformUtility.RectangleContainsScreenPoint(this.GetComponent<RectTransform>(), touch.position))
    //         {
    //             return;
    //         }
    //         // print("Move");
    //         var swipeDirection = touch.position - prevTouchPos;
    //         OnSwipe?.Invoke(swipeDirection);
    //         prevTouchPos = touch.position;
    //     }
    //     if (touch.phase == TouchPhase.Stationary)
    //     {
    //         prevTouchPos = touch.position;
    //         OnEndSwipe?.Invoke();
    //     }
    //      if (touch.phase == TouchPhase.Ended)
    //     {
    //         OnEndSwipe?.Invoke();
    //     }
    //
    //     if (touch.phase == TouchPhase.Canceled)
    //     {
    //         OnEndSwipe?.Invoke();
    //     }
    // }
}
