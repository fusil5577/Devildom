using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputController controller;
    public Rigidbody2D movementRigidbody;
    public GroundCheck groundCheck;
    public GameObject playerAttackBoxPrefab;
    private CharacterStatHandler characterStatHandler;

    public Vector2 movementDirection = Vector2.zero;
    private bool canDoubleJump = false;
    public float jumpForce = 7f;
    private SpriteRenderer sprite;

    private void Awake()
    {
        controller = GetComponent<InputController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        characterStatHandler = GetComponent<CharacterStatHandler>();

        controller.OnMoveEvent += Move;
        controller.OnJumpEvent += Jump;
        controller.OnAttackEvent += Attack;
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
        if (groundCheck.GetGroundedState() || groundCheck.GetHilledState())
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

    private void Attack()
    {
        AttackSo rangedAttackSo = characterStatHandler.CurrentStat.attackSO;
             
        GameObject obj = Instantiate(playerAttackBoxPrefab);
        PlayerAttackBox abox = obj.GetComponent<PlayerAttackBox>();
        abox.Initialize((PlayerDefaultAttackSO)rangedAttackSo);
        controller.isAttacking = true;
        
    }

    private void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        direction *= characterStatHandler.CurrentStat.speed;
        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }
}
