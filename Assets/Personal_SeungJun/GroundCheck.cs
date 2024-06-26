using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public LayerMask hillLayerMask;

    private bool isGrounded = false;
    private bool isHilled = false;
    private bool wasGrounded = false;
    private bool wasHilled = false;

    private CapsuleCollider2D capsuleCollider2D;

    public event System.Action OnGroundedEvent;

    private void Start()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        CheckGrounded();

        if ((isGrounded && !wasGrounded) || (isHilled && !wasHilled))
        {
            OnGroundedEvent?.Invoke();
        }

        wasGrounded = isGrounded;
        wasHilled = isHilled;
    }

    private void CheckGrounded()
    {
        Vector2 leftOrigin = new Vector2(transform.position.x - capsuleCollider2D.bounds.extents.x, transform.position.y - 2.99f);
        Vector2 rightOrigin = new Vector2(transform.position.x + capsuleCollider2D.bounds.extents.x, transform.position.y - 2.99f);

        RaycastHit2D groundleftHit = Physics2D.Raycast(leftOrigin, Vector2.down, 0.00001f, groundLayerMask);
        RaycastHit2D groundrightHit = Physics2D.Raycast(rightOrigin, Vector2.down, 0.00001f, groundLayerMask);

        RaycastHit2D hillleftHit = Physics2D.Raycast(leftOrigin, Vector2.down, 0.00001f, hillLayerMask);
        RaycastHit2D hillrightHit = Physics2D.Raycast(rightOrigin, Vector2.down, 0.00001f, hillLayerMask);

        // ¶¥¿¡ ´ê¾Æ ÀÖ´ÂÁö È®ÀÎ
        isGrounded = groundleftHit.collider != null || groundrightHit.collider != null;
        isHilled = hillleftHit.collider != null || hillrightHit.collider != null;
    }

    public bool GetGroundedState()
    {
        return isGrounded;
    }

    public bool GetHilledState()
    {
        return isHilled;
    }
}
