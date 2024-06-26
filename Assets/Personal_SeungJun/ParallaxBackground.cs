using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform playerTransform; // �÷��̾� ��ġ
    public Vector2 parallaxSpeed = new Vector2(0.5f, 0.1f); // ����� X��, Y�� �̵� �ӵ�

    private Vector2 startPosition; // ���� ��ġ

    void Start()
    {
        // ����� ���� ��ġ ����
        startPosition = new Vector2(transform.position.x, transform.position.y);

        playerTransform = GameManager.Instance.currentPlayer.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {  
            // �÷��̾ ������ �� ����� �ݴ� �������� �̵�
            Vector2 distanceMoved = new Vector2(
                (playerTransform.position.x - startPosition.x) * parallaxSpeed.x,
                (playerTransform.position.y - startPosition.y) * parallaxSpeed.y);

            transform.position = new Vector3(startPosition.x + distanceMoved.x, startPosition.y + distanceMoved.y, transform.position.z);
        }
    }

    public void UpdatePlayerTransform(Transform newPlayerTransform)
    {
        playerTransform = newPlayerTransform;
    }
}