using UnityEngine;

public sealed class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 14f, 12f);

    [SerializeField, Min(0f)]
    private float followSharpness = 7f;

    [SerializeField]
    private float lookAtHeight = 0.5f;

    private void LateUpdate()
    {
        if (followTarget == null)
        {
            return;
        }

        Vector3 targetPosition = followTarget.position;
        Vector3 desiredPosition = targetPosition + offset;
        float smoothing = 1f - Mathf.Exp(
            -Mathf.Max(0f, followSharpness) * Time.deltaTime);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothing);

        Vector3 lookTarget = targetPosition + Vector3.up * lookAtHeight;
        Vector3 lookDirection = lookTarget - transform.position;

        if (lookDirection.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(
                lookDirection,
                Vector3.up);
        }
    }
}
