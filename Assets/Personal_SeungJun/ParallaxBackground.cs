using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 Transform
    public Vector2 parallaxSpeed = new Vector2(0.5f, 0.1f); // 배경의 X축과 Y축 이동 속도

    private Vector2 startPosition; // 시작 위치 (X, Y 좌표만 사용)

    void Start()
    {
        // 배경의 시작 위치 기록
        startPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        // 플레이어가 움직일 때 배경도 반대 방향으로 이동
        Vector2 distanceMoved = new Vector2(
            (playerTransform.position.x - startPosition.x) * parallaxSpeed.x,
            (playerTransform.position.y - startPosition.y) * parallaxSpeed.y);

        transform.position = new Vector3(startPosition.x + distanceMoved.x, startPosition.y + distanceMoved.y, transform.position.z);
    }
}