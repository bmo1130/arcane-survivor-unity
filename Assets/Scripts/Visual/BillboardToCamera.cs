using UnityEngine;

public sealed class BillboardToCamera : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;

    public void SetCamera(Transform newCameraTransform)
    {
        cameraTransform = newCameraTransform;
    }

    private void LateUpdate()
    {
        if (cameraTransform == null)
        {
            return;
        }

        Vector3 toCamera = cameraTransform.position - transform.position;

        if (toCamera.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(
            -toCamera,
            cameraTransform.up);
    }
}
