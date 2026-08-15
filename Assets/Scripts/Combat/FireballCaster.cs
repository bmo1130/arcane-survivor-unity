using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class FireballCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;
    private const float MultiProjectileSpacing = 0.24f;
    private const string FireballSkillId = "fireball";

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

    private readonly List<SlimeController> projectileTargets = new();
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

        int projectileCount = 1
            + SchoolSynergyUtility.GetArcaneProjectileBonus(skillLoadout);
        ProjectileTargetingUtility.GetNearestAliveTargets(
            enemySpawner.SpawnedEnemies,
            transform.position,
            projectileCount,
            projectileTargets);

        if (projectileTargets.Count == 0)
        {
            return;
        }

        Vector3 firstDirection = projectileTargets[0].transform.position
            - transform.position;
        firstDirection.y = 0f;

        if (firstDirection.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        float explosionRadius = fireballLevel >= 2
            ? levelTwoExplosionRadius
            : levelOneExplosionRadius;
        float modifiedDirectDamage = SchoolSynergyUtility
            .GetModifiedMagicDamage(
                skillLoadout,
                playerMagicPower,
                directDamage);
        Vector3 launchCenter = transform.position
            + Vector3.up * LaunchHeight;
        Vector3 lateralDirection = Vector3.Cross(
            Vector3.up,
            firstDirection.normalized);

        for (int index = 0; index < projectileCount; index++)
        {
            Vector3 direction = projectileTargets[index].transform.position
                - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude <= 0.0001f)
            {
                direction = firstDirection;
            }

            float offset = projectileCount == 1
                ? 0f
                : (index - (projectileCount - 1) * 0.5f)
                    * MultiProjectileSpacing;
            FireballProjectile projectile = Instantiate(
                projectilePrefab,
                launchCenter + lateralDirection * offset,
                Quaternion.identity);

            if (!projectile.Setup(
                    direction.normalized,
                    modifiedDirectDamage,
                    projectileSpeed,
                    projectileLifetime,
                    projectileCollisionRadius,
                    explosionRadius,
                    burnDamage,
                    burnDuration,
                    burnTickInterval,
                    enemySpawner,
                    playerMagicPower,
                    skillLoadout,
                    enemySpawner.BillboardCamera))
            {
                enabled = false;
                Destroy(projectile.gameObject);
                return;
            }
        }

        cooldownRemaining = skillLoadout
            .GetModifiedSpellCooldown(cooldown);
    }

    private void DisableWithError(string message)
    {
        Debug.LogError(message, this);
        enabled = false;
    }
}
