using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform mainCameraTransform;
    private float positionZ = -10f;
    private float positionY = 3f;

    private bool isInBossZone = false;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private void Start()
    {
        mainCameraTransform = transform;
    }

    private void Update()
    {
        if (mainCameraTransform != null && TestManager.Instance.currentPlayer != null)
        {
            Vector3 playerPosition = TestManager.Instance.currentPlayer.transform.position;
            Vector3 mainCameraPosition = new Vector3(playerPosition.x, playerPosition.y + positionY, positionZ);

            if (isInBossZone)
            {
                // 보스전일 때 카메라 위치 제한
                mainCameraPosition.x = Mathf.Clamp(mainCameraPosition.x, minBounds.x, maxBounds.x);
                mainCameraPosition.y = Mathf.Clamp(mainCameraPosition.y, minBounds.y, maxBounds.y);
            }

            mainCameraTransform.position = mainCameraPosition;
        }
    }

    public void EnterBossZone(Vector3 min, Vector3 max)
    {
        isInBossZone = true;
        minBounds = min;
        maxBounds = max;
    }

    public void ExitBossZone()
    {
        isInBossZone = false;
    }
}
