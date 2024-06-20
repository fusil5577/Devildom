using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputController controller;
    private Rigidbody2D movementRigidbody;
    private GroundCheck groundCheck;

    private Vector2 movementDirection = Vector2.zero;
    private bool canDoubleJump = false;
    private float jumpForce = 7f;
    private SpriteRenderer sprite;

    [SerializeField] private float moveSpeed = 8.0f;

    private void Awake()
    {
        controller = GetComponent<InputController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>(); // GroundCheck 컴포넌트를 가져옴
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        controller.OnMoveEvent += Move;
        controller.OnJumpEvent += Jump;
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;

        if (direction.x > 0)
        {
            sprite.flipX = false;
        }
        else if (direction.x < 0)
        {
            sprite.flipX = true;
        }
    }

    private void Jump()
    {
        if (groundCheck.isGrounded || groundCheck.isGroundedOnHill)
        {
            movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, jumpForce);
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            movementRigidbody.velocity = new Vector2(movementRigidbody.velocity.x, jumpForce);
            canDoubleJump = false;
        }
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction = direction * moveSpeed;
        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }
}
