using System;
using System.Collections.Generic;
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

    [SerializeField, Min(0f)]
    private float experienceReward = 4f;

    [SerializeField, Min(0f)]
    private float separationRadius = 0.75f;

    [SerializeField, Min(0f)]
    private float separationStrength = 0.35f;

    [SerializeField]
    private bool isBoss;

    private float attackCooldownRemaining;
    private bool isDead;
    private IReadOnlyList<SlimeController> separationNeighbors;
    private Vector3 overlapFallbackDirection;
    private ExperienceOrb experienceOrbPrefab;
    private PlayerExperience playerExperience;
    private Transform billboardCamera;
    private StaggerStatus staggerStatus;
    private SlowStatus slowStatus;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;
    public bool IsAlive => !isDead && currentHealth > 0f;
    public bool IsBoss => isBoss;

    public event Action<SlimeController> Died;

    private void Awake()
    {
        maximumHealth = Mathf.Max(0f, maximumHealth);
        currentHealth = maximumHealth;
        staggerStatus = GetComponent<StaggerStatus>();
        slowStatus = GetComponent<SlowStatus>();

        int fallbackIndex = unchecked(GetInstanceID() * 397) & 1023;
        float fallbackAngle = fallbackIndex
            * (Mathf.PI * 2f / 1024f);
        overlapFallbackDirection = new Vector3(
            Mathf.Cos(fallbackAngle),
            0f,
            Mathf.Sin(fallbackAngle));
    }

    private void Start()
    {
        ValidateReferences();
    }

    public bool Setup(
        Transform newTarget,
        PlayerHealth newPlayerHealth,
        IReadOnlyList<SlimeController> newSeparationNeighbors,
        ExperienceOrb newExperienceOrbPrefab,
        PlayerExperience newPlayerExperience,
        Transform newBillboardCamera,
        bool newIsBoss)
    {
        target = newTarget;
        playerHealth = newPlayerHealth;
        separationNeighbors = newSeparationNeighbors;
        experienceOrbPrefab = newExperienceOrbPrefab;
        playerExperience = newPlayerExperience;
        billboardCamera = newBillboardCamera;
        isBoss = newIsBoss;

        return ValidateReferences();
    }

    private bool ValidateReferences()
    {
        if (target == null)
        {
            Debug.LogError(
                "SlimeController requires a target Transform.",
                this);
            enabled = false;
            return false;
        }

        if (playerHealth == null)
        {
            Debug.LogError(
                "SlimeController requires a PlayerHealth reference.",
                this);
            enabled = false;
            return false;
        }

        if (experienceOrbPrefab == null)
        {
            Debug.LogError(
                "SlimeController requires an Experience Orb Prefab.",
                this);
            enabled = false;
            return false;
        }

        if (playerExperience == null)
        {
            Debug.LogError(
                "SlimeController requires a PlayerExperience reference.",
                this);
            enabled = false;
            return false;
        }

        if (billboardCamera == null)
        {
            Debug.LogError(
                "SlimeController requires a Billboard Camera Transform.",
                this);
            enabled = false;
            return false;
        }

        enabled = true;
        return true;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || isDead
            || target == null
            || playerHealth == null
            || (staggerStatus != null && staggerStatus.IsStaggered))
        {
            return;
        }

        float attackSpeedMultiplier = slowStatus != null
            ? slowStatus.AttackSpeedMultiplier
            : 1f;
        attackCooldownRemaining = Mathf.Max(
            0f,
            attackCooldownRemaining
                - Time.deltaTime * attackSpeedMultiplier);

        Vector3 currentPosition = transform.position;
        Vector3 toTarget = target.position - currentPosition;
        toTarget.y = 0f;

        float distance = toTarget.magnitude;
        float minimumDistance = Mathf.Max(0f, stopDistance);
        float moveMultiplier = slowStatus != null
            ? slowStatus.MoveMultiplier
            : 1f;
        float maxMoveDistance = Mathf.Max(0f, moveSpeed)
            * moveMultiplier
            * Time.deltaTime;
        Vector3 chaseMovement = Vector3.zero;

        if (distance <= minimumDistance)
        {
            TryAttack();
        }
        else
        {
            float moveDistance = Mathf.Min(
                maxMoveDistance,
                distance - minimumDistance);
            chaseMovement = toTarget / distance * moveDistance;
        }

        Vector3 separationMovement = CalculateSeparationDirection()
            * (maxMoveDistance * Mathf.Max(0f, separationStrength));
        Vector3 movement = Vector3.ClampMagnitude(
            chaseMovement + separationMovement,
            maxMoveDistance);

        if (distance > 0.0001f)
        {
            Vector3 towardTarget = toTarget / distance;
            float towardMovement = Vector3.Dot(movement, towardTarget);
            float remainingDistance = Mathf.Max(
                0f,
                distance - minimumDistance);

            if (towardMovement > remainingDistance)
            {
                movement -= towardTarget
                    * (towardMovement - remainingDistance);
            }
        }

        movement.y = 0f;

        transform.position = currentPosition + movement;
    }

    private Vector3 CalculateSeparationDirection()
    {
        float radius = Mathf.Max(0f, separationRadius);

        if (radius <= 0f || separationNeighbors == null)
        {
            return Vector3.zero;
        }

        float radiusSquared = radius * radius;
        Vector3 separation = Vector3.zero;
        int neighborCount = 0;

        foreach (SlimeController neighbor in separationNeighbors)
        {
            if (neighbor == null || neighbor == this || neighbor.isDead)
            {
                continue;
            }

            Vector3 awayFromNeighbor = transform.position
                - neighbor.transform.position;
            awayFromNeighbor.y = 0f;

            float distanceSquared = awayFromNeighbor.sqrMagnitude;

            if (distanceSquared >= radiusSquared)
            {
                continue;
            }

            if (distanceSquared <= 0.0001f)
            {
                separation += overlapFallbackDirection;
            }
            else
            {
                float distance = Mathf.Sqrt(distanceSquared);
                float proximity = 1f - distance / radius;
                separation += awayFromNeighbor / distance * proximity;
            }

            neighborCount++;
        }

        if (neighborCount == 0)
        {
            return Vector3.zero;
        }

        return Vector3.ClampMagnitude(
            separation / neighborCount,
            1f);
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

    // Temporary Editor verification hook for manual damage checks.
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

        if (!isBoss)
        {
            SpawnExperienceOrb();
        }

        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private void SpawnExperienceOrb()
    {
        if (experienceOrbPrefab == null
            || target == null
            || playerExperience == null
            || billboardCamera == null)
        {
            Debug.LogError(
                "SlimeController could not spawn its Experience Orb because a runtime reference is missing.",
                this);
            return;
        }

        ExperienceOrb experienceOrb = Instantiate(
            experienceOrbPrefab,
            transform.position,
            Quaternion.identity);

        if (!experienceOrb.Setup(
                target,
                playerExperience,
                billboardCamera,
                experienceReward))
        {
            Destroy(experienceOrb.gameObject);
        }
    }
}
