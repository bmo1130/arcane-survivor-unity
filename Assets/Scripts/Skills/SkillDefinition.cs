using System;

public enum SkillSchool
{
    Arcane,
    Fire,
    Lightning,
    Frost
}

public sealed class SkillDefinition
{
    public const int SchoolSkillMaximumLevel = 2;

    public string Id { get; }
    public string DisplayName { get; }
    public SkillSchool School { get; }
    public SkillType Type { get; }
    public int MaxLevel { get; }

    public SkillDefinition(
        string id,
        string displayName,
        SkillSchool school,
        SkillType type,
        int maxLevel)
    {
        Id = RequireText(id, nameof(id));
        DisplayName = RequireText(displayName, nameof(displayName));

        if (!IsSupportedSchool(school))
        {
            throw new ArgumentOutOfRangeException(nameof(school));
        }

        if (type != SkillType.Active && type != SkillType.Passive)
        {
            throw new ArgumentOutOfRangeException(nameof(type));
        }

        if (maxLevel <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(maxLevel),
                "Skill maximum level must be positive.");
        }

        School = school;
        Type = type;
        MaxLevel = maxLevel;
    }

    private static string RequireText(string value, string parameterName)
    {
        string normalizedValue = value?.Trim();

        if (string.IsNullOrEmpty(normalizedValue))
        {
            throw new ArgumentException(
                "Skill metadata text cannot be null or empty.",
                parameterName);
        }

        return normalizedValue;
    }

    private static bool IsSupportedSchool(SkillSchool school)
    {
        return school == SkillSchool.Arcane
            || school == SkillSchool.Fire
            || school == SkillSchool.Lightning
            || school == SkillSchool.Frost;
    }
}
