using UnityEngine;

public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float maximumHealth = 100f;

    [SerializeField, Min(0f)]
    private float currentHealth;

    [SerializeField, Min(0f)]
    private float healthRegeneration;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;
    public float HealthRegeneration => healthRegeneration;

    private void Awake()
    {
        maximumHealth = IsFinite(maximumHealth)
            ? Mathf.Max(0f, maximumHealth)
            : 100f;
        currentHealth = maximumHealth;
        healthRegeneration = IsFinite(healthRegeneration)
            ? Mathf.Max(0f, healthRegeneration)
            : 0f;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || currentHealth <= 0f
            || currentHealth >= maximumHealth
            || healthRegeneration <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Min(
            maximumHealth,
            currentHealth + healthRegeneration * Time.deltaTime);
    }

    public void TakeDamage(float amount)
    {
        if (!IsFinite(amount) || amount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - amount);
    }

    public void IncreaseMaximumHealth(float amount)
    {
        if (!IsFinite(amount) || amount <= 0f)
        {
            return;
        }

        maximumHealth = AddWithoutInfinity(maximumHealth, amount);
        currentHealth = Mathf.Min(
            maximumHealth,
            AddWithoutInfinity(currentHealth, amount));
    }

    public void IncreaseHealthRegeneration(float amount)
    {
        if (!IsFinite(amount) || amount <= 0f)
        {
            return;
        }

        healthRegeneration = AddWithoutInfinity(
            healthRegeneration,
            amount);
    }

    private static float AddWithoutInfinity(float currentValue, float amount)
    {
        double result = (double)currentValue + amount;
        return result >= float.MaxValue
            ? float.MaxValue
            : (float)result;
    }

    private static bool IsFinite(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
}
