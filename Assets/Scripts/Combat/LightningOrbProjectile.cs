using System.Collections.Generic;
using UnityEngine;

public sealed class LightningOrbProjectile : MonoBehaviour
{
    private const string LightningOrbSkillId = "lightning-orb";
    private const string LightningMasterySkillId = "lightning-mastery";

    private Vector3 direction;
    private EnemySpawner enemySpawner;
    private PlayerMagicPower playerMagicPower;
    private SkillLoadout skillLoadout;
    private BillboardToCamera[] billboards;
    private float baseDamage;
    private float speed;
    private float lifetime;
    private float pulseInterval;
    private float pulseRadius;
    private float bounceRange;
    private float staggerDuration;
    private float age;
    private float pulseTimer;
    private float fixedHeight;
    private bool isInitialized;
    private bool isFinished;

    private void Awake()
    {
        billboards = GetComponentsInChildren<BillboardToCamera>(true);
        fixedHeight = transform.position.y;
    }

    public bool Setup(
        Vector3 newDirection,
        float newDamage,
        float newSpeed,
        float newLifetime,
        float newPulseInterval,
        float newPulseRadius,
        float newBounceRange,
        float newStaggerDuration,
        EnemySpawner newEnemySpawner,
        PlayerMagicPower newPlayerMagicPower,
        SkillLoadout newSkillLoadout,
        Transform billboardCamera)
    {
        newDirection.y = 0f;

        if (!IsFinite(newDirection)
            || newDirection.sqrMagnitude <= 0.0001f
            || !IsPositiveFinite(newDamage)
            || !IsNonNegativeFinite(newSpeed)
            || !IsPositiveFinite(newLifetime)
            || !IsPositiveFinite(newPulseInterval)
            || !IsNonNegativeFinite(newPulseRadius)
            || !IsNonNegativeFinite(newBounceRange)
            || !IsNonNegativeFinite(newStaggerDuration))
        {
            Debug.LogError(
                "LightningOrbProjectile received invalid gameplay values.",
                this);
            return false;
        }

        if (newEnemySpawner == null
            || newPlayerMagicPower == null
            || newSkillLoadout == null
            || billboardCamera == null)
        {
            Debug.LogError(
                "LightningOrbProjectile requires EnemySpawner, PlayerMagicPower, SkillLoadout, and Billboard Camera references.",
                this);
            return false;
        }

        if (billboards.Length == 0)
        {
            Debug.LogError(
                "LightningOrbProjectile requires BillboardToCamera in its hierarchy.",
                this);
            return false;
        }

        direction = newDirection.normalized;
        baseDamage = newDamage;
        speed = newSpeed;
        lifetime = newLifetime;
        pulseInterval = newPulseInterval;
        pulseRadius = newPulseRadius;
        bounceRange = newBounceRange;
        staggerDuration = newStaggerDuration;
        enemySpawner = newEnemySpawner;
        playerMagicPower = newPlayerMagicPower;
        skillLoadout = newSkillLoadout;
        pulseTimer = pulseInterval;
        fixedHeight = transform.position.y;

        foreach (BillboardToCamera billboard in billboards)
        {
            billboard.SetCamera(billboardCamera);
        }

        isInitialized = true;
        return true;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || !isInitialized
            || isFinished)
        {
            return;
        }

        if (enemySpawner == null
            || playerMagicPower == null
            || skillLoadout == null)
        {
            Finish();
            return;
        }

        float activeDeltaTime = Mathf.Min(
            Time.deltaTime,
            Mathf.Max(0f, lifetime - age));
        age += activeDeltaTime;

        Vector3 nextPosition = transform.position
            + direction * (speed * activeDeltaTime);
        nextPosition.y = fixedHeight;
        transform.position = nextPosition;

        pulseTimer -= activeDeltaTime;

        while (pulseTimer <= 0f && !isFinished)
        {
            Pulse();
            pulseTimer += pulseInterval;
        }

        if (age >= lifetime)
        {
            Finish();
        }
    }

    private void Pulse()
    {
        SlimeController target = FindNearestPulseTarget();

        if (target == null)
        {
            return;
        }

        int orbLevel = skillLoadout.GetSkillLevel(LightningOrbSkillId);
        int baseBounceCount = orbLevel >= 2 ? 1 : 0;
        int masteryBounceBonus = Mathf.Clamp(
            skillLoadout.GetSkillLevel(LightningMasterySkillId),
            0,
            SkillLoadout.MaximumSkillLevel);
        float modifiedDamage = SchoolSynergyUtility
            .GetModifiedMagicDamage(
                skillLoadout,
                playerMagicPower,
                baseDamage);

        LightningChainUtility.Strike(
            target,
            enemySpawner.SpawnedEnemies,
            modifiedDamage,
            baseBounceCount + masteryBounceBonus,
            bounceRange,
            staggerDuration,
            skillLoadout);
    }

    private SlimeController FindNearestPulseTarget()
    {
        IReadOnlyList<SlimeController> enemies = enemySpawner.SpawnedEnemies;
        SlimeController nearestEnemy = null;
        float nearestDistanceSquared = float.PositiveInfinity;
        float pulseRadiusSquared = pulseRadius * pulseRadius;
        Vector3 orbPosition = transform.position;

        for (int index = 0; index < enemies.Count; index++)
        {
            SlimeController enemy = enemies[index];

            if (enemy == null || !enemy.IsAlive)
            {
                continue;
            }

            Vector3 toEnemy = enemy.transform.position - orbPosition;
            toEnemy.y = 0f;
            float distanceSquared = toEnemy.sqrMagnitude;

            if (distanceSquared > pulseRadiusSquared
                || distanceSquared >= nearestDistanceSquared)
            {
                continue;
            }

            nearestDistanceSquared = distanceSquared;
            nearestEnemy = enemy;
        }

        return nearestEnemy;
    }

    private void Finish()
    {
        if (isFinished)
        {
            return;
        }

        isFinished = true;
        enabled = false;
        Destroy(gameObject);
    }

    private static bool IsFinite(Vector3 value)
    {
        return IsFinite(value.x)
            && IsFinite(value.y)
            && IsFinite(value.z);
    }

    private static bool IsNonNegativeFinite(float value)
    {
        return IsFinite(value) && value >= 0f;
    }

    private static bool IsPositiveFinite(float value)
    {
        return IsFinite(value) && value > 0f;
    }

    private static bool IsFinite(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
}
