using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public LayerMask hillLayerMask;

    public bool isGrounded = false;
    public bool isGroundedOnHill = false;
    private Collider2D currentHillCollider;

    private void Update()
    {
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        Vector2 leftOrigin = new Vector2(transform.position.x - boxCollider.bounds.extents.x, transform.position.y - 2.99f);
        Vector2 rightOrigin = new Vector2(transform.position.x + boxCollider.bounds.extents.x, transform.position.y - 2.99f);
        
        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, 0.00001f, groundLayerMask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, 0.00001f, groundLayerMask);

        // ���� ��� �ִ��� Ȯ��
        isGrounded = leftHit.collider != null || rightHit.collider != null;

        // Hill ���̾ ���� ����ĳ��Ʈ ����
        RaycastHit2D leftHillhit = Physics2D.Raycast(leftOrigin, Vector2.down, 0.1f, hillLayerMask);
        RaycastHit2D rightHillhit = Physics2D.Raycast(rightOrigin, Vector2.down, 0.1f, hillLayerMask);

        // Hill�� ��� �ִ��� ���θ� Ȯ��
        if (leftHillhit.collider != null || rightHillhit.collider != null)
        {
            isGroundedOnHill = true;

            // Hill ��ü�� �����ϰ� �ʱ� Ʈ���� ���¸� ���
            if (leftHillhit.collider != null && (currentHillCollider == null || currentHillCollider != leftHillhit.collider))
            {
                currentHillCollider = leftHillhit.collider; // Collider ��ü�� ���
                currentHillCollider.isTrigger = false;
            }
            else if (rightHillhit.collider != null && (currentHillCollider == null || currentHillCollider != rightHillhit.collider))
            {
                currentHillCollider = rightHillhit.collider; // Collider ��ü�� ���
                currentHillCollider.isTrigger = false;
            }
        }
        else
        {
            // Hill���� ����� ���� Hill�� Ʈ���� ���¸� ����
            if (isGroundedOnHill && currentHillCollider != null)
            {
                currentHillCollider.isTrigger = true;
                currentHillCollider = null;
                isGroundedOnHill = false;
            }
        }
    }
}
