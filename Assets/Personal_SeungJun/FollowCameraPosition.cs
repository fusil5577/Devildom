using UnityEngine;

public class FollowCameraPosition : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // ���� ī�޶��� Transform ��������
        mainCameraTransform = Camera.main.transform;

        // ��� �̹��� ��ġ �ʱ�ȭ
        transform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, transform.position.z);
    }

    void Update()
    {
        // ���� ī�޶� ������ ������ ��� �̹��� ��ġ ������Ʈ
        transform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, transform.position.z);
    }
}
