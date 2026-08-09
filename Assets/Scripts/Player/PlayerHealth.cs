using UnityEngine;

public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float maximumHealth = 100f;

    [SerializeField, Min(0f)]
    private float currentHealth;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;

    private void Awake()
    {
        maximumHealth = Mathf.Max(0f, maximumHealth);
        currentHealth = maximumHealth;
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - amount);
    }
}
