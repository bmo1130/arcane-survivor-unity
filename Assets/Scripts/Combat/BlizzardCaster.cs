using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class BlizzardCaster : MonoBehaviour
{
    private const string BlizzardSkillId = "blizzard";

    [SerializeField]
    private BlizzardArea areaPrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [Header("Blizzard")]
    [SerializeField, Min(0f)]
    private float damage = 1f;

    [SerializeField, Min(0f)]
    private float cooldown = 4.5f;

    [SerializeField, Min(0f)]
    private float duration = 4f;

    [SerializeField, Min(0f)]
    private float tickInterval = 1f;

    [SerializeField, Min(0f)]
    private float levelOneRadius = 2.4f;

    [SerializeField, Min(0f)]
    private float levelTwoRadius = 3.6f;

    [Header("Slow")]
    [SerializeField, Min(0f)]
    private float slowDuration = 2.5f;

    [SerializeField, Range(0f, 1f)]
    private float baseSlowMoveMultiplier = 0.7f;

    [SerializeField, Range(0f, 1f)]
    private float frostMasteryAttackSpeedMultiplier = 0.65f;

    [SerializeField, Range(0f, 1f)]
    private float frostMasteryLevelTwoMoveMultiplier = 0.5f;

    private float cooldownRemaining;

    private void Awake()
    {
        if (areaPrefab == null)
        {
            DisableWithError("BlizzardCaster requires an Area Prefab.");
            return;
        }

        if (enemySpawner == null)
        {
            DisableWithError("BlizzardCaster requires an EnemySpawner reference.");
            return;
        }

        if (playerMagicPower == null)
        {
            DisableWithError("BlizzardCaster requires PlayerMagicPower on the Player.");
            return;
        }

        if (skillLoadout == null)
        {
            DisableWithError("BlizzardCaster requires a SkillLoadout reference.");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        int skillLevel = skillLoadout.GetSkillLevel(BlizzardSkillId);

        if (skillLevel <= 0)
        {
            return;
        }

        cooldownRemaining = Mathf.Max(
            0f,
            cooldownRemaining - Time.deltaTime);

        if (cooldownRemaining > 0f)
        {
            return;
        }

        SlimeController target = FindNearestAliveSlime();

        if (target == null)
        {
            return;
        }

        float radius = skillLevel >= 2
            ? levelTwoRadius
            : levelOneRadius;
        Vector3 areaPosition = target.transform.position;
        areaPosition.y = 0f;
        BlizzardArea area = Instantiate(
            areaPrefab,
            areaPosition,
            Quaternion.identity);

        if (!area.Setup(
                radius,
                duration,
                tickInterval,
                damage,
                slowDuration,
                baseSlowMoveMultiplier,
                frostMasteryAttackSpeedMultiplier,
                frostMasteryLevelTwoMoveMultiplier,
                enemySpawner,
                playerMagicPower,
                skillLoadout))
        {
            enabled = false;
            Destroy(area.gameObject);
            return;
        }

        cooldownRemaining = skillLoadout
            .GetModifiedSpellCooldown(cooldown);
    }

    private SlimeController FindNearestAliveSlime()
    {
        IReadOnlyList<SlimeController> enemies = enemySpawner.SpawnedEnemies;
        SlimeController nearestEnemy = null;
        float nearestDistanceSquared = float.PositiveInfinity;
        Vector3 casterPosition = transform.position;

        for (int index = 0; index < enemies.Count; index++)
        {
            SlimeController enemy = enemies[index];

            if (enemy == null || !enemy.IsAlive)
            {
                continue;
            }

            Vector3 toEnemy = enemy.transform.position - casterPosition;
            toEnemy.y = 0f;
            float distanceSquared = toEnemy.sqrMagnitude;

            if (distanceSquared < nearestDistanceSquared)
            {
                nearestDistanceSquared = distanceSquared;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void DisableWithError(string message)
    {
        Debug.LogError(message, this);
        enabled = false;
    }
}
