using System.Collections.Generic;
using UnityEngine;

// Small shared selector for the five projectile casters affected by Arcane synergy.
public static class ProjectileTargetingUtility
{
    public static void GetNearestAliveTargets(
        IReadOnlyList<SlimeController> enemies,
        Vector3 origin,
        int targetCount,
        List<SlimeController> results)
    {
        results.Clear();

        if (enemies == null || targetCount <= 0)
        {
            return;
        }

        for (int slotIndex = 0; slotIndex < targetCount; slotIndex++)
        {
            SlimeController nearestEnemy = null;
            float nearestDistanceSquared = float.PositiveInfinity;

            for (int enemyIndex = 0;
                enemyIndex < enemies.Count;
                enemyIndex++)
            {
                SlimeController enemy = enemies[enemyIndex];

                if (enemy == null
                    || !enemy.IsAlive
                    || results.Contains(enemy))
                {
                    continue;
                }

                Vector3 toEnemy = enemy.transform.position - origin;
                toEnemy.y = 0f;
                float distanceSquared = toEnemy.sqrMagnitude;

                if (distanceSquared < nearestDistanceSquared)
                {
                    nearestDistanceSquared = distanceSquared;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy == null)
            {
                break;
            }

            results.Add(nearestEnemy);
        }

        int distinctTargetCount = results.Count;

        if (distinctTargetCount == 0)
        {
            return;
        }

        while (results.Count < targetCount)
        {
            results.Add(results[results.Count % distinctTargetCount]);
        }
    }
}
