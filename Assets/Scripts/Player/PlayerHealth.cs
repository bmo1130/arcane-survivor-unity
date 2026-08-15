using System;
using UnityEngine;

public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float maximumHealth = 100f;

    [SerializeField, Min(0f)]
    private float currentHealth;

    [SerializeField, Min(0f)]
    private float healthRegeneration;

    [SerializeField]
    private bool isDead;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;
    public float HealthRegeneration => healthRegeneration;
    public bool IsDead => isDead;

    public event Action Died;

    private void Awake()
    {
        maximumHealth = IsFinite(maximumHealth)
            ? Mathf.Max(0f, maximumHealth)
            : 100f;
        currentHealth = maximumHealth;
        isDead = currentHealth <= 0f;
        healthRegeneration = IsFinite(healthRegeneration)
            ? Mathf.Max(0f, healthRegeneration)
            : 0f;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || isDead
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
        if (isDead || !IsFinite(amount) || amount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - amount);

        if (currentHealth <= 0f)
        {
            isDead = true;
            Died?.Invoke();
        }
    }

    public void IncreaseMaximumHealth(float amount)
    {
        if (!IsFinite(amount) || amount <= 0f)
        {
            return;
        }

        maximumHealth = AddWithoutInfinity(maximumHealth, amount);

        if (!isDead)
        {
            currentHealth = Mathf.Min(
                maximumHealth,
                AddWithoutInfinity(currentHealth, amount));
        }
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

    // Temporary Editor verification hook for the U13-A Defeat flow.
    [ContextMenu("Debug Take Lethal Damage")]
    private void DebugTakeLethalDamage()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Take Lethal Damage is available only in Play Mode.",
                this);
            return;
        }

        if (!isDead)
        {
            TakeDamage(Mathf.Max(currentHealth, 1f));
        }
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
