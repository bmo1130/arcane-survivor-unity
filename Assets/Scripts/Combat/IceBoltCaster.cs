using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class IceBoltCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;
    private const float MultiProjectileSpacing = 0.22f;
    private const string IceBoltSkillId = "ice-bolt";

    [SerializeField]
    private IceBoltProjectile projectilePrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [Header("Ice Bolt")]
    [SerializeField, Min(0f)]
    private float damage = 1f;

    [SerializeField, Min(0f)]
    private float cooldown = 0.85f;

    [SerializeField, Min(0f)]
    private float projectileSpeed = 8f;

    [SerializeField, Min(0f)]
    private float projectileLifetime = 4f;

    [SerializeField, Min(0f)]
    private float projectileCollisionRadius = 0.2f;

    [SerializeField, Min(0f)]
    private float levelTwoAreaRadius = 1.8f;

    [Header("Slow")]
    [SerializeField, Min(0f)]
    private float slowDuration = 2.5f;

    [SerializeField, Range(0f, 1f)]
    private float baseSlowMoveMultiplier = 0.7f;

    [SerializeField, Range(0f, 1f)]
    private float frostMasteryAttackSpeedMultiplier = 0.65f;

    [SerializeField, Range(0f, 1f)]
    private float frostMasteryLevelTwoMoveMultiplier = 0.5f;

    private readonly List<SlimeController> projectileTargets = new();
    private float cooldownRemaining;

    private void Awake()
    {
        if (projectilePrefab == null)
        {
            DisableWithError("IceBoltCaster requires a Projectile Prefab.");
            return;
        }

        if (enemySpawner == null)
        {
            DisableWithError("IceBoltCaster requires an EnemySpawner reference.");
            return;
        }

        if (playerMagicPower == null)
        {
            DisableWithError("IceBoltCaster requires PlayerMagicPower on the Player.");
            return;
        }

        if (skillLoadout == null)
        {
            DisableWithError("IceBoltCaster requires a SkillLoadout reference.");
            return;
        }

        if (enemySpawner.BillboardCamera == null)
        {
            DisableWithError("IceBoltCaster requires the EnemySpawner Billboard Camera.");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || skillLoadout.GetSkillLevel(IceBoltSkillId) <= 0)
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
            IceBoltProjectile projectile = Instantiate(
                projectilePrefab,
                launchCenter + lateralDirection * offset,
                Quaternion.identity);

            if (!projectile.Setup(
                    direction.normalized,
                    damage,
                    projectileSpeed,
                    projectileLifetime,
                    projectileCollisionRadius,
                    levelTwoAreaRadius,
                    slowDuration,
                    baseSlowMoveMultiplier,
                    frostMasteryAttackSpeedMultiplier,
                    frostMasteryLevelTwoMoveMultiplier,
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
