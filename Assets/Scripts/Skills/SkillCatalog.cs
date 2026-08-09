using System;
using System.Collections.Generic;

public static class SkillCatalog
{
    private const int MaxLevel = SkillDefinition.SchoolSkillMaximumLevel;

    private static readonly SkillDefinition[] Definitions =
    {
        new(
            "magic-missile",
            "Magic Missile",
            SkillSchool.Arcane,
            SkillType.Active,
            MaxLevel),
        new(
            "magic-bolt",
            "Magic Bolt",
            SkillSchool.Arcane,
            SkillType.Active,
            MaxLevel),
        new(
            "arcane-mastery",
            "Arcane Mastery",
            SkillSchool.Arcane,
            SkillType.Passive,
            MaxLevel),
        new(
            "fireball",
            "Fireball",
            SkillSchool.Fire,
            SkillType.Active,
            MaxLevel),
        new(
            "fire-zone",
            "Fire Zone",
            SkillSchool.Fire,
            SkillType.Active,
            MaxLevel),
        new(
            "fire-mastery",
            "Fire Mastery",
            SkillSchool.Fire,
            SkillType.Passive,
            MaxLevel),
        new(
            "chain-lightning",
            "Chain Lightning",
            SkillSchool.Lightning,
            SkillType.Active,
            MaxLevel),
        new(
            "lightning-orb",
            "Lightning Orb",
            SkillSchool.Lightning,
            SkillType.Active,
            MaxLevel),
        new(
            "lightning-mastery",
            "Lightning Mastery",
            SkillSchool.Lightning,
            SkillType.Passive,
            MaxLevel),
        new(
            "ice-bolt",
            "Ice Bolt",
            SkillSchool.Frost,
            SkillType.Active,
            MaxLevel),
        new(
            "blizzard",
            "Blizzard",
            SkillSchool.Frost,
            SkillType.Active,
            MaxLevel),
        new(
            "frost-mastery",
            "Frost Mastery",
            SkillSchool.Frost,
            SkillType.Passive,
            MaxLevel)
    };

    private static readonly IReadOnlyList<SkillDefinition> ReadOnlyDefinitions =
        Array.AsReadOnly(Definitions);

    private static readonly Dictionary<string, SkillDefinition> DefinitionsById =
        CreateDefinitionsById();

    public static IReadOnlyList<SkillDefinition> All => ReadOnlyDefinitions;

    public static bool TryGet(
        string skillId,
        out SkillDefinition definition)
    {
        definition = null;

        if (string.IsNullOrWhiteSpace(skillId))
        {
            return false;
        }

        return DefinitionsById.TryGetValue(skillId.Trim(), out definition);
    }

    private static Dictionary<string, SkillDefinition> CreateDefinitionsById()
    {
        Dictionary<string, SkillDefinition> definitionsById =
            new(StringComparer.Ordinal);

        foreach (SkillDefinition definition in Definitions)
        {
            definitionsById.Add(definition.Id, definition);
        }

        return definitionsById;
    }
}
