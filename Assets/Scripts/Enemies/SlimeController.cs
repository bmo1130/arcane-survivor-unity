using UnityEngine;

public sealed class SlimeController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField, Min(0f)]
    private float moveSpeed = 2.6f;

    [SerializeField, Min(0f)]
    private float stopDistance = 1.15f;

    private void Awake()
    {
        if (target != null)
        {
            return;
        }

        Debug.LogError(
            "SlimeController requires a target Transform.",
            this);
        enabled = false;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 currentPosition = transform.position;
        Vector3 toTarget = target.position - currentPosition;
        toTarget.y = 0f;

        float distance = toTarget.magnitude;
        float minimumDistance = Mathf.Max(0f, stopDistance);

        if (distance <= minimumDistance)
        {
            return;
        }

        float maxMoveDistance = Mathf.Max(0f, moveSpeed) * Time.deltaTime;
        float moveDistance = Mathf.Min(
            maxMoveDistance,
            distance - minimumDistance);

        transform.position = currentPosition
            + toTarget / distance * moveDistance;
    }
}
