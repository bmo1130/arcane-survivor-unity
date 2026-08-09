using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class FireballCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;
    private const string FireballSkillId = "fireball";
    private const string FireMasterySkillId = "fire-mastery";

    [SerializeField]
    private FireballProjectile projectilePrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [Header("Fireball")]
    [SerializeField, Min(0f)]
    private float directDamage = 1f;

    [SerializeField, Min(0f)]
    private float cooldown = 1.35f;

    [SerializeField, Min(0f)]
    private float projectileSpeed = 7.5f;

    [SerializeField, Min(0f)]
    private float projectileLifetime = 4f;

    [SerializeField, Min(0f)]
    private float projectileCollisionRadius = 0.22f;

    [SerializeField, Min(0f)]
    private float levelOneExplosionRadius = 2.2f;

    [SerializeField, Min(0f)]
    private float levelTwoExplosionRadius = 3.4f;

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
        if (projectilePrefab == null)
        {
            DisableWithError("FireballCaster requires a Projectile Prefab.");
            return;
        }

        if (enemySpawner == null)
        {
            DisableWithError("FireballCaster requires an EnemySpawner reference.");
            return;
        }

        if (playerMagicPower == null)
        {
            DisableWithError("FireballCaster requires PlayerMagicPower on the Player.");
            return;
        }

        if (skillLoadout == null)
        {
            DisableWithError("FireballCaster requires a SkillLoadout reference.");
            return;
        }

        if (enemySpawner.BillboardCamera == null)
        {
            DisableWithError("FireballCaster requires the EnemySpawner Billboard Camera.");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        int fireballLevel = skillLoadout.GetSkillLevel(FireballSkillId);

        if (fireballLevel <= 0)
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

        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        float explosionRadius = fireballLevel >= 2
            ? levelTwoExplosionRadius
            : levelOneExplosionRadius;
        float modifiedDirectDamage = playerMagicPower
            .GetModifiedMagicDamage(directDamage);
        float modifiedBurnDamage = playerMagicPower
            .GetModifiedMagicDamage(
                burnDamage + GetFireMasteryBurnBonus());
        Vector3 launchPosition = transform.position
            + Vector3.up * LaunchHeight;
        FireballProjectile projectile = Instantiate(
            projectilePrefab,
            launchPosition,
            Quaternion.identity);

        if (!projectile.Setup(
                direction.normalized,
                modifiedDirectDamage,
                projectileSpeed,
                projectileLifetime,
                projectileCollisionRadius,
                explosionRadius,
                modifiedBurnDamage,
                burnDuration,
                burnTickInterval,
                enemySpawner,
                enemySpawner.BillboardCamera))
        {
            enabled = false;
            Destroy(projectile.gameObject);
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
