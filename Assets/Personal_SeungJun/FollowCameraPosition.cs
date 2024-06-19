using UnityEngine;

public class FollowCameraPosition : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // 메인 카메라의 Transform 가져오기
        mainCameraTransform = Camera.main.transform;

        // 배경 이미지 위치 초기화
        transform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, transform.position.z);
    }

    void Update()
    {
        // 메인 카메라가 움직일 때마다 배경 이미지 위치 업데이트
        transform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, transform.position.z);
    }
}
