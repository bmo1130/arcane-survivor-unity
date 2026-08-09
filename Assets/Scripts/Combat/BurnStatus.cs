using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SlimeController))]
public sealed class BurnStatus : MonoBehaviour
{
    [SerializeField]
    private bool isBurning;

    [SerializeField, Min(0f)]
    private float remainingDuration;

    [SerializeField, Min(0f)]
    private float currentTickDamage;

    [SerializeField, Min(0f)]
    private float currentTickInterval;

    [SerializeField, Min(0f)]
    private float tickProgress;

    private SlimeController slime;

    public bool IsBurning => isBurning;
    public float RemainingDuration => remainingDuration;

    private void Awake()
    {
        slime = GetComponent<SlimeController>();

        if (slime == null)
        {
            Debug.LogError(
                "BurnStatus requires SlimeController on the same GameObject.",
                this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || !isBurning
            || slime == null)
        {
            return;
        }

        if (!slime.IsAlive)
        {
            ClearBurn();
            return;
        }

        float deltaTime = Time.deltaTime;
        remainingDuration = Mathf.Max(0f, remainingDuration - deltaTime);
        tickProgress += deltaTime;

        while (isBurning
            && slime.IsAlive
            && tickProgress >= currentTickInterval)
        {
            tickProgress -= currentTickInterval;
            slime.TakeDamage(currentTickDamage);
        }

        if (remainingDuration <= 0f || !slime.IsAlive)
        {
            ClearBurn();
        }
    }

    public void ApplyBurn(
        float tickDamage,
        float duration,
        float tickInterval)
    {
        if (!IsPositiveFinite(tickDamage)
            || !IsPositiveFinite(duration)
            || !IsPositiveFinite(tickInterval)
            || slime == null
            || !slime.IsAlive)
        {
            return;
        }

        bool wasBurning = isBurning;
        isBurning = true;
        remainingDuration = duration;
        currentTickDamage = tickDamage;
        currentTickInterval = tickInterval;

        if (!wasBurning)
        {
            tickProgress = 0f;
        }
    }

    private void ClearBurn()
    {
        isBurning = false;
        remainingDuration = 0f;
        currentTickDamage = 0f;
        currentTickInterval = 0f;
        tickProgress = 0f;
    }

    private static bool IsPositiveFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value > 0f;
    }
}
