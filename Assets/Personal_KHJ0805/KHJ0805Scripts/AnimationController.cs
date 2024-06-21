using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private GroundCheck groundCheck;

    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        groundCheck = GetComponentInParent<GroundCheck>();
    }

    private void Update()
    {
        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        animator.SetBool(IsMoving, playerMovement.movementDirection.x != 0);

        if (playerMovement.movementRigidbody.velocity.y > 0 && !groundCheck.isGrounded && !groundCheck.isGroundedOnHill)
        {
            animator.SetBool(IsJumping, true);
            animator.SetBool(IsFalling, false);
        }
        else if (playerMovement.movementRigidbody.velocity.y < 0 && !groundCheck.isGrounded && !groundCheck.isGroundedOnHill)
        {
            animator.SetBool(IsJumping, false);
            animator.SetBool(IsFalling, true);
        }
        else if (groundCheck.isGrounded || groundCheck.isGroundedOnHill)
        {
            animator.SetBool(IsJumping, false);
            animator.SetBool(IsFalling, false);
        }
    }
}
