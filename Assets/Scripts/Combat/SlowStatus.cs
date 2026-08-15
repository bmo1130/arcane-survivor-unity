using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SlimeController))]
public sealed class SlowStatus : MonoBehaviour
{
    [SerializeField]
    private bool isSlowed;

    [SerializeField, Min(0f)]
    private float remainingDuration;

    [SerializeField, Range(0f, 1f)]
    private float moveMultiplier = 1f;

    [SerializeField, Range(0f, 1f)]
    private float currentMoveMultiplier = 1f;

    [SerializeField, Range(0f, 1f)]
    private float attackSpeedMultiplier = 1f;

    [SerializeField, Min(0f)]
    private float elapsedSlowTime;

    [SerializeField]
    private bool progressiveSlowActive;

    private float progressiveSlowPerSecond;
    private float minimumMoveMultiplier = 1f;

    public bool IsSlowed => isSlowed;
    public float RemainingDuration => remainingDuration;
    public float MoveMultiplier => isSlowed
        ? currentMoveMultiplier
        : 1f;
    public float AttackSpeedMultiplier => isSlowed
        ? attackSpeedMultiplier
        : 1f;

    public void ApplySlow(
        float duration,
        float newMoveMultiplier,
        float newAttackSpeedMultiplier,
        bool newProgressiveSlowActive,
        float newProgressiveSlowPerSecond,
        float newMinimumMoveMultiplier)
    {
        if (!IsPositiveFinite(duration)
            || !IsValidMultiplier(newMoveMultiplier)
            || !IsValidMultiplier(newAttackSpeedMultiplier)
            || !IsNonNegativeFinite(newProgressiveSlowPerSecond)
            || !IsValidMultiplier(newMinimumMoveMultiplier))
        {
            return;
        }

        bool wasSlowed = isSlowed;
        remainingDuration = duration;
        moveMultiplier = newMoveMultiplier;
        attackSpeedMultiplier = newAttackSpeedMultiplier;
        progressiveSlowActive = newProgressiveSlowActive;
        progressiveSlowPerSecond = newProgressiveSlowPerSecond;
        minimumMoveMultiplier = newMinimumMoveMultiplier;
        isSlowed = true;

        if (!wasSlowed)
        {
            elapsedSlowTime = 0f;
        }

        UpdateCurrentMoveMultiplier();
    }

    private void Update()
    {
        if (Time.timeScale <= 0f || !isSlowed)
        {
            return;
        }

        remainingDuration = Mathf.Max(
            0f,
            remainingDuration - Time.deltaTime);
        elapsedSlowTime += Time.deltaTime;
        UpdateCurrentMoveMultiplier();

        if (remainingDuration <= 0f)
        {
            ClearSlow();
        }
    }

    private void ClearSlow()
    {
        isSlowed = false;
        remainingDuration = 0f;
        moveMultiplier = 1f;
        currentMoveMultiplier = 1f;
        attackSpeedMultiplier = 1f;
        elapsedSlowTime = 0f;
        progressiveSlowActive = false;
        progressiveSlowPerSecond = 0f;
        minimumMoveMultiplier = 1f;
    }

    private void UpdateCurrentMoveMultiplier()
    {
        currentMoveMultiplier = progressiveSlowActive
            ? Mathf.Max(
                minimumMoveMultiplier,
                moveMultiplier
                    - elapsedSlowTime * progressiveSlowPerSecond)
            : moveMultiplier;
    }

    private static bool IsValidMultiplier(float value)
    {
        return IsPositiveFinite(value) && value <= 1f;
    }

    private static bool IsPositiveFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value > 0f;
    }

    private static bool IsNonNegativeFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value >= 0f;
    }
}
