using System.Collections.Generic;
using UnityEngine;

public sealed class FireZoneArea : MonoBehaviour
{
    [SerializeField]
    private Transform visual;

    private EnemySpawner enemySpawner;
    private float radius;
    private float duration;
    private float applyInterval;
    private float burnDamage;
    private float burnDuration;
    private float burnTickInterval;
    private float age;
    private float applyTimer;
    private bool isInitialized;
    private bool isFinished;

    public bool Setup(
        float newRadius,
        float newDuration,
        float newApplyInterval,
        float newBurnDamage,
        float newBurnDuration,
        float newBurnTickInterval,
        EnemySpawner newEnemySpawner)
    {
        if (!IsNonNegativeFinite(newRadius)
            || !IsPositiveFinite(newDuration)
            || !IsPositiveFinite(newApplyInterval)
            || !IsPositiveFinite(newBurnDamage)
            || !IsPositiveFinite(newBurnDuration)
            || !IsPositiveFinite(newBurnTickInterval))
        {
            Debug.LogError(
                "FireZoneArea received invalid gameplay values.",
                this);
            return false;
        }

        if (newEnemySpawner == null || visual == null)
        {
            Debug.LogError(
                "FireZoneArea requires EnemySpawner and Visual references.",
                this);
            return false;
        }

        radius = newRadius;
        duration = newDuration;
        applyInterval = newApplyInterval;
        burnDamage = newBurnDamage;
        burnDuration = newBurnDuration;
        burnTickInterval = newBurnTickInterval;
        enemySpawner = newEnemySpawner;
        applyTimer = 0f;

        float diameter = radius * 2f;
        visual.localScale = new Vector3(diameter, diameter, 1f);

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

        if (enemySpawner == null)
        {
            Finish();
            return;
        }

        age += Time.deltaTime;

        if (age >= duration)
        {
            Finish();
            return;
        }

        applyTimer -= Time.deltaTime;

        if (applyTimer > 0f)
        {
            return;
        }

        ApplyBurnInRadius();
        applyTimer = applyInterval;
    }

    private void ApplyBurnInRadius()
    {
        IReadOnlyList<SlimeController> enemies = enemySpawner.SpawnedEnemies;
        float radiusSquared = radius * radius;
        Vector3 areaPosition = transform.position;

        for (int index = 0; index < enemies.Count; index++)
        {
            SlimeController enemy = enemies[index];

            if (enemy == null || !enemy.IsAlive)
            {
                continue;
            }

            Vector3 toEnemy = enemy.transform.position - areaPosition;
            toEnemy.y = 0f;

            if (toEnemy.sqrMagnitude > radiusSquared)
            {
                continue;
            }

            if (enemy.TryGetComponent(out BurnStatus burnStatus))
            {
                burnStatus.ApplyBurn(
                    burnDamage,
                    burnDuration,
                    burnTickInterval);
            }
        }
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
