using UnityEngine;
using UnityEngine.UI;


public class JumpButton : MonoBehaviour
{
    public Button jumpBtn;

    public void Start()
    {
        jumpBtn = gameObject.GetComponent<Button>();
        jumpBtn.onClick.AddListener(() =>
        {
            InputManager.Instance.HandleJumpButtonInput();
        });
    }
}
