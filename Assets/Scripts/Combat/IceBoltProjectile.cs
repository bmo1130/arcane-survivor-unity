using System.Collections.Generic;
using UnityEngine;

public sealed class IceBoltProjectile : MonoBehaviour
{
    private const string IceBoltSkillId = "ice-bolt";

    private Vector3 direction;
    private EnemySpawner enemySpawner;
    private PlayerMagicPower playerMagicPower;
    private SkillLoadout skillLoadout;
    private BillboardToCamera[] billboards;
    private float baseDamage;
    private float speed;
    private float lifetime;
    private float collisionRadius;
    private float levelTwoAreaRadius;
    private float slowDuration;
    private float baseSlowMoveMultiplier;
    private float frostMasteryAttackSpeedMultiplier;
    private float frostMasteryLevelTwoMoveMultiplier;
    private float age;
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
        float newCollisionRadius,
        float newLevelTwoAreaRadius,
        float newSlowDuration,
        float newBaseSlowMoveMultiplier,
        float newFrostMasteryAttackSpeedMultiplier,
        float newFrostMasteryLevelTwoMoveMultiplier,
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
            || !IsNonNegativeFinite(newCollisionRadius)
            || !IsNonNegativeFinite(newLevelTwoAreaRadius)
            || !IsPositiveFinite(newSlowDuration)
            || !IsSlowMultiplier(newBaseSlowMoveMultiplier)
            || !IsSlowMultiplier(newFrostMasteryAttackSpeedMultiplier)
            || !IsSlowMultiplier(newFrostMasteryLevelTwoMoveMultiplier))
        {
            Debug.LogError(
                "IceBoltProjectile received invalid gameplay values.",
                this);
            return false;
        }

        if (newEnemySpawner == null
            || newPlayerMagicPower == null
            || newSkillLoadout == null
            || billboardCamera == null)
        {
            Debug.LogError(
                "IceBoltProjectile requires EnemySpawner, PlayerMagicPower, SkillLoadout, and Billboard Camera references.",
                this);
            return false;
        }

        if (billboards.Length == 0)
        {
            Debug.LogError(
                "IceBoltProjectile requires BillboardToCamera in its hierarchy.",
                this);
            return false;
        }

        direction = newDirection.normalized;
        baseDamage = newDamage;
        speed = newSpeed;
        lifetime = newLifetime;
        collisionRadius = newCollisionRadius;
        levelTwoAreaRadius = newLevelTwoAreaRadius;
        slowDuration = newSlowDuration;
        baseSlowMoveMultiplier = newBaseSlowMoveMultiplier;
        frostMasteryAttackSpeedMultiplier =
            newFrostMasteryAttackSpeedMultiplier;
        frostMasteryLevelTwoMoveMultiplier =
            newFrostMasteryLevelTwoMoveMultiplier;
        enemySpawner = newEnemySpawner;
        playerMagicPower = newPlayerMagicPower;
        skillLoadout = newSkillLoadout;
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

        age += Time.deltaTime;

        if (age >= lifetime)
        {
            Finish();
            return;
        }

        Vector3 startPosition = transform.position;
        startPosition.y = fixedHeight;
        Vector3 endPosition = startPosition
            + direction * (speed * Time.deltaTime);
        endPosition.y = fixedHeight;

        SlimeController hitEnemy = FindFirstHitOnSegment(
            startPosition,
            endPosition,
            out float hitProgress);

        if (hitEnemy == null)
        {
            transform.position = endPosition;
            return;
        }

        transform.position = Vector3.Lerp(
            startPosition,
            endPosition,
            hitProgress);

        if (skillLoadout.GetSkillLevel(IceBoltSkillId) >= 2)
        {
            ApplyLevelTwoImpact(hitEnemy);
        }
        else
        {
            ApplyHit(hitEnemy);
        }

        Finish();
    }

    private void ApplyLevelTwoImpact(SlimeController directTarget)
    {
        ApplyHit(directTarget);

        IReadOnlyList<SlimeController> enemies = enemySpawner.SpawnedEnemies;
        float radiusSquared = levelTwoAreaRadius * levelTwoAreaRadius;
        Vector3 impactPosition = transform.position;

        for (int index = 0; index < enemies.Count; index++)
        {
            SlimeController enemy = enemies[index];

            if (enemy == null
                || enemy == directTarget
                || !enemy.IsAlive)
            {
                continue;
            }

            Vector3 toEnemy = enemy.transform.position - impactPosition;
            toEnemy.y = 0f;

            if (toEnemy.sqrMagnitude <= radiusSquared)
            {
                ApplyHit(enemy);
            }
        }
    }

    private void ApplyHit(SlimeController enemy)
    {
        if (enemy == null || !enemy.IsAlive)
        {
            return;
        }

        float modifiedDamage = SchoolSynergyUtility
            .GetModifiedMagicDamage(
                skillLoadout,
                playerMagicPower,
                baseDamage);
        enemy.TakeDamage(modifiedDamage);

        FrostSlowUtility.ApplySlow(
            enemy,
            skillLoadout,
            slowDuration,
            baseSlowMoveMultiplier,
            frostMasteryAttackSpeedMultiplier,
            frostMasteryLevelTwoMoveMultiplier);
    }

    private SlimeController FindFirstHitOnSegment(
        Vector3 startPosition,
        Vector3 endPosition,
        out float hitProgress)
    {
        IReadOnlyList<SlimeController> enemies = enemySpawner.SpawnedEnemies;
        SlimeController firstHit = null;
        float firstHitProgress = float.PositiveInfinity;
        float collisionRadiusSquared = collisionRadius * collisionRadius;
        Vector3 segment = endPosition - startPosition;
        segment.y = 0f;
        float segmentLengthSquared = segment.sqrMagnitude;

        for (int index = 0; index < enemies.Count; index++)
        {
            SlimeController enemy = enemies[index];

            if (enemy == null || !enemy.IsAlive)
            {
                continue;
            }

            if (!TryGetSegmentHitProgress(
                    startPosition,
                    segment,
                    segmentLengthSquared,
                    enemy.transform.position,
                    collisionRadiusSquared,
                    out float enemyHitProgress)
                || enemyHitProgress >= firstHitProgress)
            {
                continue;
            }

            firstHit = enemy;
            firstHitProgress = enemyHitProgress;
        }

        hitProgress = firstHit != null ? firstHitProgress : 1f;
        return firstHit;
    }

    private static bool TryGetSegmentHitProgress(
        Vector3 startPosition,
        Vector3 segment,
        float segmentLengthSquared,
        Vector3 enemyPosition,
        float collisionRadiusSquared,
        out float hitProgress)
    {
        Vector3 fromEnemy = startPosition - enemyPosition;
        fromEnemy.y = 0f;
        float startDistanceSquared = fromEnemy.sqrMagnitude;

        if (startDistanceSquared <= collisionRadiusSquared)
        {
            hitProgress = 0f;
            return true;
        }

        if (segmentLengthSquared <= 0.000001f)
        {
            hitProgress = 0f;
            return false;
        }

        float doubledProjection = 2f * Vector3.Dot(fromEnemy, segment);
        float distanceFromRadius = startDistanceSquared
            - collisionRadiusSquared;
        float discriminant = doubledProjection * doubledProjection
            - 4f * segmentLengthSquared * distanceFromRadius;

        if (discriminant < 0f)
        {
            hitProgress = 0f;
            return false;
        }

        float firstIntersection = (-doubledProjection
            - Mathf.Sqrt(discriminant))
            / (2f * segmentLengthSquared);

        if (firstIntersection < 0f || firstIntersection > 1f)
        {
            hitProgress = 0f;
            return false;
        }

        hitProgress = firstIntersection;
        return true;
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

    private static bool IsSlowMultiplier(float value)
    {
        return IsPositiveFinite(value) && value <= 1f;
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
