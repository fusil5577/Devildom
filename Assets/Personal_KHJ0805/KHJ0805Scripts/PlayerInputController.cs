using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : InputController
{
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnJump(InputValue value)
    {
        CallJumpEvent();
    }
}
