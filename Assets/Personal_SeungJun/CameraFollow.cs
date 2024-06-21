using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = transform;
    }

    private void Update()
    {
        if (mainCameraTransform != null && TestManager.Instance.currentPlayer != null)
        {
            Vector3 mainCameraPosition = TestManager.Instance.currentPlayer.transform.position;
            mainCameraPosition.z = -10; // z 값 -10으로 고정 (카메라가 안보여서)
            mainCameraTransform.position = mainCameraPosition;
        }
    }
}
