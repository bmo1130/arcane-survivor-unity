using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class ChainLightningCaster : MonoBehaviour
{
    private const string ChainLightningSkillId = "chain-lightning";
    private const string LightningMasterySkillId = "lightning-mastery";

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField]
    private SkillLoadout skillLoadout;

    [SerializeField, Min(0f)]
    private float damage = 1f;

    [SerializeField, Min(0f)]
    private float levelTwoDamageBonus = 1f;

    [SerializeField, Min(0f)]
    private float cooldown = 1.1f;

    [SerializeField, Min(0)]
    private int baseBounceCount = 2;

    [SerializeField, Min(0f)]
    private float bounceRange = 5.5f;

    [SerializeField, Min(0f)]
    private float staggerDuration = 0.1f;

    private float cooldownRemaining;

    private void Awake()
    {
        if (enemySpawner == null)
        {
            DisableWithError(
                "ChainLightningCaster requires an EnemySpawner reference.");
            return;
        }

        if (playerMagicPower == null)
        {
            DisableWithError(
                "ChainLightningCaster requires PlayerMagicPower on the Player.");
            return;
        }

        if (skillLoadout == null)
        {
            DisableWithError(
                "ChainLightningCaster requires a SkillLoadout reference.");
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        int skillLevel = skillLoadout.GetSkillLevel(
            ChainLightningSkillId);

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

        float levelDamage = damage
            + (skillLevel >= 2 ? levelTwoDamageBonus : 0f);
        float modifiedDamage = SchoolSynergyUtility
            .GetModifiedMagicDamage(
                skillLoadout,
                playerMagicPower,
                levelDamage);
        int bounceCount = Mathf.Max(0, baseBounceCount)
            + GetLightningMasteryBounceBonus();

        LightningChainUtility.Strike(
            target,
            enemySpawner.SpawnedEnemies,
            modifiedDamage,
            bounceCount,
            Mathf.Max(0f, bounceRange),
            Mathf.Max(0f, staggerDuration),
            skillLoadout);

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

    private int GetLightningMasteryBounceBonus()
    {
        return Mathf.Clamp(
            skillLoadout.GetSkillLevel(LightningMasterySkillId),
            0,
            SkillLoadout.MaximumSkillLevel);
    }

    private void DisableWithError(string message)
    {
        Debug.LogError(message, this);
        enabled = false;
    }
}
