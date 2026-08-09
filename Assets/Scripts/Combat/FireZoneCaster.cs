using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class FireZoneCaster : MonoBehaviour
{
    private const string FireZoneSkillId = "fire-zone";
    private const string FireMasterySkillId = "fire-mastery";

    [SerializeField]
    private FireZoneArea areaPrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [Header("Fire Zone")]
    [SerializeField, Min(0f)]
    private float cooldown = 4f;

    [SerializeField, Min(0f)]
    private float duration = 4f;

    [SerializeField, Min(0f)]
    private float burnApplyInterval = 0.5f;

    [SerializeField, Min(0f)]
    private float levelOneRadius = 2.2f;

    [SerializeField, Min(0f)]
    private float levelTwoRadius = 3.5f;

    [Header("Burning")]
    [SerializeField, Min(0f)]
    private float burnDamage = 1f;

    [SerializeField, Min(0f)]
    private float burnDuration = 3f;

    [SerializeField, Min(0f)]
    private float burnTickInterval = 1f;

    private float cooldownRemaining;

    private void Awake()
    {
        if (areaPrefab == null)
        {
            DisableWithError("FireZoneCaster requires an Area Prefab.");
            return;
        }

        if (enemySpawner == null)
        {
            DisableWithError("FireZoneCaster requires an EnemySpawner reference.");
            return;
        }

        if (playerMagicPower == null)
        {
            DisableWithError("FireZoneCaster requires PlayerMagicPower on the Player.");
            return;
        }

        if (skillLoadout == null)
        {
            DisableWithError("FireZoneCaster requires a SkillLoadout reference.");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        int fireZoneLevel = skillLoadout.GetSkillLevel(FireZoneSkillId);

        if (fireZoneLevel <= 0)
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

        float radius = fireZoneLevel >= 2
            ? levelTwoRadius
            : levelOneRadius;
        float modifiedBurnDamage = playerMagicPower
            .GetModifiedMagicDamage(
                burnDamage + GetFireMasteryBurnBonus());
        Vector3 areaPosition = target.transform.position;
        areaPosition.y = 0f;
        FireZoneArea area = Instantiate(
            areaPrefab,
            areaPosition,
            Quaternion.identity);

        if (!area.Setup(
                radius,
                duration,
                burnApplyInterval,
                modifiedBurnDamage,
                burnDuration,
                burnTickInterval,
                enemySpawner))
        {
            enabled = false;
            Destroy(area.gameObject);
            return;
        }

        cooldownRemaining = Mathf.Max(0f, cooldown);
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

    private float GetFireMasteryBurnBonus()
    {
        int masteryLevel = skillLoadout.GetSkillLevel(FireMasterySkillId);

        if (masteryLevel >= 2)
        {
            return 3f;
        }

        return masteryLevel >= 1 ? 1f : 0f;
    }

    private void DisableWithError(string message)
    {
        Debug.LogError(message, this);
        enabled = false;
    }
}
