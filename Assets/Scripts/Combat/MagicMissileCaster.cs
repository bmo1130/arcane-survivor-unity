using System.Collections.Generic;
using UnityEngine;

// Runs the existing Magic Missile combat only while the Skill is equipped.
[DisallowMultipleComponent]
public sealed class MagicMissileCaster : MonoBehaviour
{
    private const float LaunchHeight = 1.25f;
    private const float LevelTwoDamageBonus = 1f;
    private const float MultiProjectileSpacing = 0.32f;
    private const string MagicMissileSkillId = "magic-missile";

    [SerializeField]
    private SkillLoadout skillLoadout;

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

    private readonly List<SlimeController> projectileTargets = new();
    private float cooldownRemaining;

    private void Awake()
    {
        if (skillLoadout == null)
        {
            Debug.LogError(
                "MagicMissileCaster requires a SkillLoadout reference.",
                this);
            enabled = false;
            return;
        }

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

        int skillLevel = skillLoadout.GetSkillLevel(
            MagicMissileSkillId);

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

        int projectileCount = (skillLevel >= 2 ? 2 : 1)
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

        float levelDamage = damage
            + (skillLevel >= 2 ? LevelTwoDamageBonus : 0f);
        float modifiedDamage = SchoolSynergyUtility
            .GetModifiedMagicDamage(
                skillLoadout,
                playerMagicPower,
                levelDamage);
        Vector3 launchCenter = transform.position
            + Vector3.up * LaunchHeight;
        Vector3 lateralDirection = GetLateralDirection(
            projectileTargets[0].transform.position - transform.position);

        for (int index = 0; index < projectileCount; index++)
        {
            SlimeController target = projectileTargets[index];
            float offset = projectileCount == 1
                ? 0f
                : (index - (projectileCount - 1) * 0.5f)
                    * MultiProjectileSpacing;
            MagicMissileProjectile projectile = Instantiate(
                projectilePrefab,
                launchCenter + lateralDirection * offset,
                Quaternion.identity);

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
        }

        cooldownRemaining = skillLoadout
            .GetModifiedSpellCooldown(cooldown);
    }

    private static Vector3 GetLateralDirection(Vector3 direction)
    {
        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.0001f)
        {
            return Vector3.right;
        }

        return Vector3.Cross(Vector3.up, direction.normalized);
    }
}
