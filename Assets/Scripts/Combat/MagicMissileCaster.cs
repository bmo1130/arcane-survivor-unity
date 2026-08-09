using System.Collections.Generic;
using UnityEngine;

// Manually attached Editor test component. This is not starting loadout logic.
[DisallowMultipleComponent]
public sealed class MagicMissileCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;

    [SerializeField]
    private MagicMissileProjectile projectilePrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField, Min(0f)]
    [Tooltip("Base damage before the Player's Magic Damage Bonus.")]
    private float damage = 3f;

    [SerializeField, Min(0f)]
    private float cooldown = 0.65f;

    [SerializeField, Min(0f)]
    private float projectileSpeed = 6f;

    [SerializeField, Min(0f)]
    private float projectileLifetime = 5f;

    [SerializeField, Min(0f)]
    private float projectileCollisionRadius = 0.22f;

    private float cooldownRemaining;

    private void Awake()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError(
                "MagicMissileCaster requires a Projectile Prefab.",
                this);
            enabled = false;
            return;
        }

        if (enemySpawner == null)
        {
            Debug.LogError(
                "MagicMissileCaster requires an EnemySpawner reference.",
                this);
            enabled = false;
            return;
        }

        if (playerMagicPower == null)
        {
            Debug.LogError(
                "MagicMissileCaster requires the Player's PlayerMagicPower component.",
                this);
            enabled = false;
            return;
        }

        if (enemySpawner.BillboardCamera == null)
        {
            Debug.LogError(
                "MagicMissileCaster requires the EnemySpawner Billboard Camera.",
                this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
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

        Vector3 launchPosition = transform.position
            + Vector3.up * LaunchHeight;
        MagicMissileProjectile projectile = Instantiate(
            projectilePrefab,
            launchPosition,
            Quaternion.identity);
        float modifiedDamage = playerMagicPower
            .GetModifiedMagicDamage(damage);

        if (!projectile.Setup(
                target,
                modifiedDamage,
                projectileSpeed,
                projectileLifetime,
                projectileCollisionRadius,
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
}
