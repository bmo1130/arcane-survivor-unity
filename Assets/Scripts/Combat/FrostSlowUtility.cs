public static class FrostSlowUtility
{
    private const string FrostMasterySkillId = "frost-mastery";

    public static bool ApplySlow(
        SlimeController enemy,
        SkillLoadout skillLoadout,
        float duration,
        float baseMoveMultiplier,
        float masteryAttackSpeedMultiplier,
        float masteryLevelTwoMoveMultiplier)
    {
        if (enemy == null
            || !enemy.IsAlive
            || skillLoadout == null
            || !IsPositiveFinite(duration)
            || !IsValidMultiplier(baseMoveMultiplier)
            || !IsValidMultiplier(masteryAttackSpeedMultiplier)
            || !IsValidMultiplier(masteryLevelTwoMoveMultiplier)
            || !enemy.TryGetComponent(out SlowStatus slowStatus))
        {
            return false;
        }

        int masteryLevel = skillLoadout.GetSkillLevel(
            FrostMasterySkillId);
        float moveMultiplier = masteryLevel >= 2
            ? masteryLevelTwoMoveMultiplier
            : baseMoveMultiplier;
        float attackSpeedMultiplier = masteryLevel >= 1
            ? masteryAttackSpeedMultiplier
            : 1f;
        float modifiedDuration = SchoolSynergyUtility
            .GetFrostSlowDuration(skillLoadout, duration);
        bool progressiveSlowActive = SchoolSynergyUtility
            .IsFrostProgressiveSlowActive(skillLoadout);

        slowStatus.ApplySlow(
            modifiedDuration,
            moveMultiplier,
            attackSpeedMultiplier,
            progressiveSlowActive,
            SchoolSynergyUtility.FrostProgressiveSlowPerSecond,
            SchoolSynergyUtility.FrostMinimumMoveMultiplier);
        return true;
    }

    private static bool IsValidMultiplier(float value)
    {
        return IsPositiveFinite(value) && value <= 1f;
    }

    private static bool IsPositiveFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value > 0f;
    }
}
