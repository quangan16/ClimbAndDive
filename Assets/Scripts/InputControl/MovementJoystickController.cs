using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementJoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private float handleRange;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector2 res;


    public void Start()
    {
        MovementJoystickArea.OnJoystickInteracted += GetInputDirection;
        MovementJoystickArea.OnJoystickOff += ResetInputDirection;
    }

    public Vector2 GetInputDirection(PointerEventData eventData)
    {
        Vector2 localHandlePosition;
        // Vector2 res = Vector3.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background, eventData.position, canvas.worldCamera, out localHandlePosition))
        {
            Vector2 handleDragDirection = localHandlePosition.normalized;
            float distance = Mathf.Min(localHandlePosition.magnitude, handleRange);

            handle.anchoredPosition = handleDragDirection * distance;
            res = handleDragDirection * (distance / handleRange);
            InputManager.Instance.HandleJoystickMovementInput(res);
        }

        return res;
    }

    public void ResetInputDirection()
    {
        InputManager.Instance.HandleJoystickMovementInput(Vector3.Lerp(res, Vector3.zero,1));
    }

    public void SetHandlePosition(Vector3 position)
    {
        handle.anchoredPosition = position;
    }

    public void ResetHandlePosition()
    {
        SetHandlePosition(Vector3.zero);
    }

}
