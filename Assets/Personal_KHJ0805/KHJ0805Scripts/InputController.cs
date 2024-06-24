using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnJumpEvent;

    public void CallMoveEvent(Vector2 direction)
    {
        if (!GameManager.Instance.isTalkingToNPC)
        {
            OnMoveEvent?.Invoke(direction);
        }
    }

    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }
}
