using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class MagicBoltCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;
    private const string MagicBoltSkillId = "magic-bolt";

    [SerializeField]
    private MagicBoltProjectile projectilePrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [SerializeField, Min(0f)]
    [Tooltip("Base damage before the Player's Magic Damage Bonus.")]
    private float damage = 4f;

    [SerializeField, Min(0f)]
    private float cooldown = 0.9f;

    [SerializeField, Min(0f)]
    private float projectileSpeed = 9f;

    [SerializeField, Min(0f)]
    private float projectileLifetime = 4f;

    [SerializeField, Min(0f)]
    private float projectileCollisionRadius = 0.2f;

    private float cooldownRemaining;

    private void Awake()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError(
                "MagicBoltCaster requires a Projectile Prefab.",
                this);
            enabled = false;
            return;
        }

        if (enemySpawner == null)
        {
            Debug.LogError(
                "MagicBoltCaster requires an EnemySpawner reference.",
                this);
            enabled = false;
            return;
        }

        if (playerMagicPower == null)
        {
            Debug.LogError(
                "MagicBoltCaster requires the Player's PlayerMagicPower component.",
                this);
            enabled = false;
            return;
        }

        if (skillLoadout == null)
        {
            Debug.LogError(
                "MagicBoltCaster requires a SkillLoadout reference.",
                this);
            enabled = false;
            return;
        }

        if (enemySpawner.BillboardCamera == null)
        {
            Debug.LogError(
                "MagicBoltCaster requires the EnemySpawner Billboard Camera.",
                this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || skillLoadout.GetSkillLevel(MagicBoltSkillId) <= 0)
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

        Vector3 launchPosition = transform.position
            + Vector3.up * LaunchHeight;
        MagicBoltProjectile projectile = Instantiate(
            projectilePrefab,
            launchPosition,
            Quaternion.identity);
        float modifiedDamage = playerMagicPower
            .GetModifiedMagicDamage(damage);

        if (!projectile.Setup(
                direction.normalized,
                modifiedDamage,
                projectileSpeed,
                projectileLifetime,
                projectileCollisionRadius,
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
}
