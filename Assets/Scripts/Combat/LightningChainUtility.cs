using System.Collections.Generic;
using UnityEngine;

public static class LightningChainUtility
{
    public static int Strike(
        SlimeController initialTarget,
        IReadOnlyList<SlimeController> enemies,
        float damage,
        int bounceCount,
        float bounceRange,
        float staggerDuration,
        SkillLoadout skillLoadout)
    {
        if (initialTarget == null
            || !initialTarget.IsAlive
            || enemies == null
            || skillLoadout == null
            || !IsPositiveFinite(damage)
            || bounceCount < 0
            || !IsNonNegativeFinite(bounceRange)
            || !IsNonNegativeFinite(staggerDuration))
        {
            return 0;
        }

        HashSet<SlimeController> hitEnemies = new();
        SlimeController currentTarget = initialTarget;
        int hitCount = 0;
        int totalBounceCount = bounceCount
            + SchoolSynergyUtility.GetLightningBounceBonus(skillLoadout);
        float effectiveStaggerDuration = SchoolSynergyUtility
            .GetLightningStaggerDuration(
                skillLoadout,
                staggerDuration);
        float bounceRangeSquared = bounceRange * bounceRange;

        for (int hitIndex = 0;
            hitIndex <= totalBounceCount && currentTarget != null;
            hitIndex++)
        {
            Vector3 bounceOrigin = currentTarget.transform.position;
            hitEnemies.Add(currentTarget);
            float hitDamage = damage + SchoolSynergyUtility
                .GetLightningHitDamageBonus(skillLoadout, hitIndex);
            currentTarget.TakeDamage(hitDamage);
            hitCount++;

            if (currentTarget.IsAlive
                && effectiveStaggerDuration > 0f
                && currentTarget.TryGetComponent(
                    out StaggerStatus staggerStatus))
            {
                staggerStatus.ApplyStagger(effectiveStaggerDuration);
            }

            if (hitIndex >= totalBounceCount)
            {
                break;
            }

            currentTarget = FindNearestBounceTarget(
                bounceOrigin,
                enemies,
                hitEnemies,
                bounceRangeSquared);
        }

        return hitCount;
    }

    private static SlimeController FindNearestBounceTarget(
        Vector3 origin,
        IReadOnlyList<SlimeController> enemies,
        HashSet<SlimeController> hitEnemies,
        float bounceRangeSquared)
    {
        SlimeController nearestEnemy = null;
        float nearestDistanceSquared = float.PositiveInfinity;

        for (int index = 0; index < enemies.Count; index++)
        {
            SlimeController enemy = enemies[index];

            if (enemy == null
                || !enemy.IsAlive
                || hitEnemies.Contains(enemy))
            {
                continue;
            }

            Vector3 toEnemy = enemy.transform.position - origin;
            toEnemy.y = 0f;
            float distanceSquared = toEnemy.sqrMagnitude;

            if (distanceSquared > bounceRangeSquared
                || distanceSquared >= nearestDistanceSquared)
            {
                continue;
            }

            nearestDistanceSquared = distanceSquared;
            nearestEnemy = enemy;
        }

        return nearestEnemy;
    }

    private static bool IsNonNegativeFinite(float value)
    {
        return IsFinite(value) && value >= 0f;
    }

    private static bool IsPositiveFinite(float value)
    {
        return IsFinite(value) && value > 0f;
    }

    private static bool IsFinite(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }
}
