using UnityEngine;

public class FollowCameraPosition : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;

        transform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, transform.position.z);
    }

    void Update()
    {
        transform.position = new Vector3(mainCameraTransform.position.x, mainCameraTransform.position.y, transform.position.z);
    }
}
