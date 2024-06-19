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

        // 땅에 닿아 있는지 확인
        isGrounded = leftHit.collider != null || rightHit.collider != null;

        // Hill 레이어에 대한 레이캐스트 검출
        RaycastHit2D leftHillhit = Physics2D.Raycast(leftOrigin, Vector2.down, 0.1f, hillLayerMask);
        RaycastHit2D rightHillhit = Physics2D.Raycast(rightOrigin, Vector2.down, 0.1f, hillLayerMask);

        // Hill을 밟고 있는지 여부를 확인
        if (leftHillhit.collider != null || rightHillhit.collider != null)
        {
            isGroundedOnHill = true;

            // Hill 객체를 저장하고 초기 트리거 상태를 기억
            if (leftHillhit.collider != null && (currentHillCollider == null || currentHillCollider != leftHillhit.collider))
            {
                currentHillCollider = leftHillhit.collider; // Collider 객체를 기억
                currentHillCollider.isTrigger = false;
            }
            else if (rightHillhit.collider != null && (currentHillCollider == null || currentHillCollider != rightHillhit.collider))
            {
                currentHillCollider = rightHillhit.collider; // Collider 객체를 기억
                currentHillCollider.isTrigger = false;
            }
        }
        else
        {
            // Hill에서 벗어나면 이전 Hill의 트리거 상태를 복원
            if (isGroundedOnHill && currentHillCollider != null)
            {
                currentHillCollider.isTrigger = true;
                currentHillCollider = null;
                isGroundedOnHill = false;
            }
        }
    }
}
