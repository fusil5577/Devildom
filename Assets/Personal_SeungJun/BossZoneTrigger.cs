using UnityEngine;

public class BossZoneTrigger : MonoBehaviour
{
    public Collider2D bossZoneCollider; // 보스전 영역
    public Collider2D cameraLimitCollider; // 카메라 범위 제한
    public Collider2D[] wallColliders;
    private CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        if (bossZoneCollider == null)
        {
            bossZoneCollider = GetComponent<Collider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cameraFollow != null)
            {
                TestManager.Instance.fadeImage.FadeOut(TestManager.Instance.Screenimage);

                Vector3 minBounds = cameraLimitCollider.bounds.min;
                Vector3 maxBounds = cameraLimitCollider.bounds.max;
                cameraFollow.EnterBossZone(minBounds, maxBounds);

                foreach (Collider2D wallCollider in wallColliders)
                {
                    if (wallCollider != null)
                    {
                        wallCollider.isTrigger = false;
                    }
                }

                TestManager.Instance.fadeImage.FadeIn(TestManager.Instance.Screenimage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (cameraFollow != null)
            {
                cameraFollow.ExitBossZone();
            }
        }
    }
}
