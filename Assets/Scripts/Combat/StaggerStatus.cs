using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SlimeController))]
public sealed class StaggerStatus : MonoBehaviour
{
    [SerializeField]
    private bool isStaggered;

    [SerializeField, Min(0f)]
    private float remainingDuration;

    public bool IsStaggered => isStaggered;
    public float RemainingDuration => remainingDuration;

    private void Update()
    {
        if (Time.timeScale <= 0f || !isStaggered)
        {
            return;
        }

        remainingDuration = Mathf.Max(
            0f,
            remainingDuration - Time.deltaTime);

        if (remainingDuration <= 0f)
        {
            isStaggered = false;
        }
    }

    public void ApplyStagger(float duration)
    {
        if (!IsPositiveFinite(duration))
        {
            return;
        }

        remainingDuration = Mathf.Max(
            remainingDuration,
            duration);
        isStaggered = remainingDuration > 0f;
    }

    private static bool IsPositiveFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value > 0f;
    }
}
