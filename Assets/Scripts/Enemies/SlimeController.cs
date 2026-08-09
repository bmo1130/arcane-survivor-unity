using UnityEngine;

public sealed class SlimeController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField, Min(0f)]
    private float moveSpeed = 2.6f;

    [SerializeField, Min(0f)]
    private float stopDistance = 1.15f;

    [SerializeField, Min(0f)]
    private float damage = 8f;

    [SerializeField, Min(0f)]
    private float attackCooldown = 1.2f;

    [SerializeField, Min(0f)]
    private float maximumHealth = 10f;

    [SerializeField, Min(0f)]
    private float currentHealth;

    [SerializeField, Min(0f)]
    private float debugDamage = 3f;

    private float attackCooldownRemaining;
    private bool isDead;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;

    private void Awake()
    {
        maximumHealth = Mathf.Max(0f, maximumHealth);
        currentHealth = maximumHealth;

        if (target == null)
        {
            Debug.LogError(
                "SlimeController requires a target Transform.",
                this);
            enabled = false;
            return;
        }

        if (playerHealth == null)
        {
            Debug.LogError(
                "SlimeController requires a PlayerHealth reference.",
                this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (isDead || target == null || playerHealth == null)
        {
            return;
        }

        attackCooldownRemaining = Mathf.Max(
            0f,
            attackCooldownRemaining - Time.deltaTime);

        Vector3 currentPosition = transform.position;
        Vector3 toTarget = target.position - currentPosition;
        toTarget.y = 0f;

        float distance = toTarget.magnitude;
        float minimumDistance = Mathf.Max(0f, stopDistance);

        if (distance <= minimumDistance)
        {
            TryAttack();
            return;
        }

        float maxMoveDistance = Mathf.Max(0f, moveSpeed) * Time.deltaTime;
        float moveDistance = Mathf.Min(
            maxMoveDistance,
            distance - minimumDistance);

        transform.position = currentPosition
            + toTarget / distance * moveDistance;
    }

    private void TryAttack()
    {
        if (attackCooldownRemaining > 0f)
        {
            return;
        }

        playerHealth.TakeDamage(Mathf.Max(0f, damage));
        attackCooldownRemaining = Mathf.Max(0f, attackCooldown);
    }

    public void TakeDamage(float amount)
    {
        if (isDead
            || float.IsNaN(amount)
            || float.IsInfinity(amount)
            || amount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Max(0f, currentHealth - amount);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Temporary Editor verification hook until spell combat calls TakeDamage.
    [ContextMenu("Debug Take Damage")]
    private void DebugTakeDamage()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Take Damage is available only in Play Mode.",
                this);
            return;
        }

        TakeDamage(debugDamage);
    }

    private void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        enabled = false;
        Destroy(gameObject);
    }
}
