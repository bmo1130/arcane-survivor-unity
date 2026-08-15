using UnityEngine;

public static class SchoolSynergyUtility
{
    public const float FireSpreadRadius = 3.2f;
    public const float FireSpreadInterval = 1f;
    public const float FrostProgressiveSlowPerSecond = 0.06f;
    public const float FrostMinimumMoveMultiplier = 0.25f;

    public static int GetArcaneProjectileBonus(SkillLoadout skillLoadout)
    {
        int points = GetPoints(skillLoadout, SkillSchool.Arcane);
        return points >= 6 ? 2 : points >= 2 ? 1 : 0;
    }

    public static float GetArcaneMagicDamageBonus(
        SkillLoadout skillLoadout)
    {
        int points = GetPoints(skillLoadout, SkillSchool.Arcane);
        return points >= 6 ? 2f : points >= 4 ? 1f : 0f;
    }

    public static float GetModifiedMagicDamage(
        SkillLoadout skillLoadout,
        PlayerMagicPower playerMagicPower,
        float baseDamage)
    {
        if (playerMagicPower == null)
        {
            return 0f;
        }

        return playerMagicPower.GetModifiedMagicDamage(
            baseDamage + GetArcaneMagicDamageBonus(skillLoadout));
    }

    public static float GetFireBurnDuration(
        SkillLoadout skillLoadout,
        float baseDuration)
    {
        return Mathf.Max(0f, baseDuration)
            + (GetPoints(skillLoadout, SkillSchool.Fire) >= 2
                ? 2f
                : 0f);
    }

    public static float GetFireBurnTickInterval(
        SkillLoadout skillLoadout,
        float baseTickInterval)
    {
        float multiplier = GetPoints(skillLoadout, SkillSchool.Fire) >= 4
            ? 0.65f
            : 1f;
        return Mathf.Max(0f, baseTickInterval) * multiplier;
    }

    public static bool IsFireSpreadActive(SkillLoadout skillLoadout)
    {
        return GetPoints(skillLoadout, SkillSchool.Fire) >= 6;
    }

    public static float GetLightningStaggerDuration(
        SkillLoadout skillLoadout,
        float baseDuration)
    {
        float duration = Mathf.Max(0f, baseDuration);
        return GetPoints(skillLoadout, SkillSchool.Lightning) >= 2
            ? Mathf.Max(duration, 0.15f)
            : duration;
    }

    public static float GetLightningHitDamageBonus(
        SkillLoadout skillLoadout,
        int hitIndex)
    {
        return GetPoints(skillLoadout, SkillSchool.Lightning) >= 4
            ? Mathf.Max(0, hitIndex)
            : 0f;
    }

    public static int GetLightningBounceBonus(
        SkillLoadout skillLoadout)
    {
        return GetPoints(skillLoadout, SkillSchool.Lightning) >= 6
            ? 2
            : 0;
    }

    public static float GetFrostSlowDuration(
        SkillLoadout skillLoadout,
        float baseDuration)
    {
        int points = GetPoints(skillLoadout, SkillSchool.Frost);
        float duration = Mathf.Max(0f, baseDuration);

        if (points >= 2)
        {
            duration += 1.5f;
        }

        if (points >= 4)
        {
            duration += 1.5f;
        }

        return duration;
    }

    public static bool IsFrostProgressiveSlowActive(
        SkillLoadout skillLoadout)
    {
        return GetPoints(skillLoadout, SkillSchool.Frost) >= 6;
    }

    private static int GetPoints(
        SkillLoadout skillLoadout,
        SkillSchool school)
    {
        return skillLoadout != null
            ? skillLoadout.GetSchoolPoints(school)
            : 0;
    }
}
