using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class LightningOrbCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;
    private const float MultiProjectileSpacing = 0.24f;
    private const string LightningOrbSkillId = "lightning-orb";

    [SerializeField]
    private LightningOrbProjectile projectilePrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [SerializeField, Min(0f)]
    private float damage = 1f;

    [SerializeField, Min(0f)]
    private float cooldown = 3f;

    [SerializeField, Min(0f)]
    private float projectileSpeed = 2.2f;

    [SerializeField, Min(0f)]
    private float projectileLifetime = 6f;

    [SerializeField, Min(0f)]
    private float pulseInterval = 0.75f;

    [SerializeField, Min(0f)]
    private float pulseRadius = 4.5f;

    [SerializeField, Min(0f)]
    private float bounceRange = 5.5f;

    [SerializeField, Min(0f)]
    private float staggerDuration = 0.1f;

    private readonly List<SlimeController> projectileTargets = new();
    private float cooldownRemaining;

    private void Awake()
    {
        if (projectilePrefab == null)
        {
            DisableWithError(
                "LightningOrbCaster requires a Projectile Prefab.");
            return;
        }

        if (enemySpawner == null)
        {
            DisableWithError(
                "LightningOrbCaster requires an EnemySpawner reference.");
            return;
        }

        if (playerMagicPower == null)
        {
            DisableWithError(
                "LightningOrbCaster requires PlayerMagicPower on the Player.");
            return;
        }

        if (skillLoadout == null)
        {
            DisableWithError(
                "LightningOrbCaster requires a SkillLoadout reference.");
            return;
        }

        if (enemySpawner.BillboardCamera == null)
        {
            DisableWithError(
                "LightningOrbCaster requires the EnemySpawner Billboard Camera.");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        int skillLevel = skillLoadout.GetSkillLevel(
            LightningOrbSkillId);

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
            LightningOrbProjectile projectile = Instantiate(
                projectilePrefab,
                launchCenter + lateralDirection * offset,
                Quaternion.identity);

            if (!projectile.Setup(
                    direction.normalized,
                    damage,
                    projectileSpeed,
                    projectileLifetime,
                    pulseInterval,
                    pulseRadius,
                    bounceRange,
                    staggerDuration,
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
