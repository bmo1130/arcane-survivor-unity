using System.Collections.Generic;
using UnityEngine;

public sealed class BlizzardArea : MonoBehaviour
{
    [SerializeField]
    private Transform visual;

    private EnemySpawner enemySpawner;
    private PlayerMagicPower playerMagicPower;
    private SkillLoadout skillLoadout;
    private float radius;
    private float duration;
    private float tickInterval;
    private float baseDamage;
    private float slowDuration;
    private float baseSlowMoveMultiplier;
    private float frostMasteryAttackSpeedMultiplier;
    private float frostMasteryLevelTwoMoveMultiplier;
    private float age;
    private float tickTimer;
    private bool isInitialized;
    private bool isFinished;

    public bool Setup(
        float newRadius,
        float newDuration,
        float newTickInterval,
        float newDamage,
        float newSlowDuration,
        float newBaseSlowMoveMultiplier,
        float newFrostMasteryAttackSpeedMultiplier,
        float newFrostMasteryLevelTwoMoveMultiplier,
        EnemySpawner newEnemySpawner,
        PlayerMagicPower newPlayerMagicPower,
        SkillLoadout newSkillLoadout)
    {
        if (!IsNonNegativeFinite(newRadius)
            || !IsPositiveFinite(newDuration)
            || !IsPositiveFinite(newTickInterval)
            || !IsPositiveFinite(newDamage)
            || !IsPositiveFinite(newSlowDuration)
            || !IsSlowMultiplier(newBaseSlowMoveMultiplier)
            || !IsSlowMultiplier(newFrostMasteryAttackSpeedMultiplier)
            || !IsSlowMultiplier(newFrostMasteryLevelTwoMoveMultiplier))
        {
            Debug.LogError(
                "BlizzardArea received invalid gameplay values.",
                this);
            return false;
        }

        if (newEnemySpawner == null
            || newPlayerMagicPower == null
            || newSkillLoadout == null
            || visual == null)
        {
            Debug.LogError(
                "BlizzardArea requires EnemySpawner, PlayerMagicPower, SkillLoadout, and Visual references.",
                this);
            return false;
        }

        radius = newRadius;
        duration = newDuration;
        tickInterval = newTickInterval;
        baseDamage = newDamage;
        slowDuration = newSlowDuration;
        baseSlowMoveMultiplier = newBaseSlowMoveMultiplier;
        frostMasteryAttackSpeedMultiplier =
            newFrostMasteryAttackSpeedMultiplier;
        frostMasteryLevelTwoMoveMultiplier =
            newFrostMasteryLevelTwoMoveMultiplier;
        enemySpawner = newEnemySpawner;
        playerMagicPower = newPlayerMagicPower;
        skillLoadout = newSkillLoadout;
        tickTimer = tickInterval;

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

        if (enemySpawner == null
            || playerMagicPower == null
            || skillLoadout == null)
        {
            Finish();
            return;
        }

        float activeDeltaTime = Mathf.Min(
            Time.deltaTime,
            Mathf.Max(0f, duration - age));
        age += activeDeltaTime;
        tickTimer -= activeDeltaTime;

        while (tickTimer <= 0f && !isFinished)
        {
            ApplyTick();
            tickTimer += tickInterval;
        }

        if (age >= duration)
        {
            Finish();
        }
    }

    private void ApplyTick()
    {
        IReadOnlyList<SlimeController> enemies = enemySpawner.SpawnedEnemies;
        float radiusSquared = radius * radius;
        float damage = SchoolSynergyUtility.GetModifiedMagicDamage(
            skillLoadout,
            playerMagicPower,
            baseDamage);
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

            enemy.TakeDamage(damage);
            FrostSlowUtility.ApplySlow(
                enemy,
                skillLoadout,
                slowDuration,
                baseSlowMoveMultiplier,
                frostMasteryAttackSpeedMultiplier,
                frostMasteryLevelTwoMoveMultiplier);
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
