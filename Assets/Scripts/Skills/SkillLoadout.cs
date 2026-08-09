using System;
using UnityEngine;

public enum SkillType
{
    Active,
    Passive
}

[DisallowMultipleComponent]
public sealed class SkillLoadout : MonoBehaviour
{
    public const int MaximumSkillLevel =
        SkillDefinition.SchoolSkillMaximumLevel;

    private const string DebugActiveAId = "debug-active-a";
    private const string DebugActiveBId = "debug-active-b";
    private const string DebugActiveCId = "debug-active-c";
    private const string DebugPassiveAId = "debug-passive-a";
    private const string DebugPassiveBId = "debug-passive-b";

    [Header("Active Slot 1")]
    [SerializeField]
    private string activeSlot1SkillId = string.Empty;

    [SerializeField, Range(0, MaximumSkillLevel)]
    private int activeSlot1Level;

    [Header("Active Slot 2")]
    [SerializeField]
    private string activeSlot2SkillId = string.Empty;

    [SerializeField, Range(0, MaximumSkillLevel)]
    private int activeSlot2Level;

    [Header("Passive Slot 1")]
    [SerializeField]
    private string passiveSlot1SkillId = string.Empty;

    [SerializeField, Range(0, MaximumSkillLevel)]
    private int passiveSlot1Level;

    public string ActiveSlot1SkillId => activeSlot1SkillId;
    public int ActiveSlot1Level => activeSlot1Level;
    public string ActiveSlot2SkillId => activeSlot2SkillId;
    public int ActiveSlot2Level => activeSlot2Level;
    public string PassiveSlot1SkillId => passiveSlot1SkillId;
    public int PassiveSlot1Level => passiveSlot1Level;
    public bool IsEmpty => string.IsNullOrEmpty(activeSlot1SkillId)
        && activeSlot1Level == 0
        && string.IsNullOrEmpty(activeSlot2SkillId)
        && activeSlot2Level == 0
        && string.IsNullOrEmpty(passiveSlot1SkillId)
        && passiveSlot1Level == 0;

    private enum SlotIndex
    {
        None,
        Active1,
        Active2,
        Passive1
    }

    private void Awake()
    {
        ResetLoadoutState();
    }

    public bool CanAcquireOrUpgrade(string skillId, SkillType type)
    {
        return CanAcquireOrUpgradeInternal(
            skillId,
            type,
            MaximumSkillLevel);
    }

    public bool CanAcquireOrUpgrade(SkillDefinition skill)
    {
        return skill != null
            && CanAcquireOrUpgradeInternal(
                skill.Id,
                skill.Type,
                skill.MaxLevel);
    }

    public bool AcquireOrUpgrade(string skillId, SkillType type)
    {
        return AcquireOrUpgradeInternal(
            skillId,
            type,
            MaximumSkillLevel);
    }

    public bool AcquireOrUpgrade(SkillDefinition skill)
    {
        return skill != null
            && AcquireOrUpgradeInternal(
                skill.Id,
                skill.Type,
                skill.MaxLevel);
    }

    public int GetSkillLevel(string skillId)
    {
        if (!TryNormalizeSkillId(skillId, out string normalizedSkillId))
        {
            return 0;
        }

        return TryFindEquippedSkill(
            normalizedSkillId,
            out _,
            out _,
            out int currentLevel)
            ? currentLevel
            : 0;
    }

    public int GetSchoolPoints(SkillSchool school)
    {
        if (!IsSupportedSchool(school))
        {
            return 0;
        }

        return GetSlotSchoolPoints(
                activeSlot1SkillId,
                activeSlot1Level,
                school)
            + GetSlotSchoolPoints(
                activeSlot2SkillId,
                activeSlot2Level,
                school)
            + GetSlotSchoolPoints(
                passiveSlot1SkillId,
                passiveSlot1Level,
                school);
    }

    private bool CanAcquireOrUpgradeInternal(
        string skillId,
        SkillType type,
        int maxLevel)
    {
        if (!TryNormalizeSkillId(skillId, out string normalizedSkillId)
            || !IsSupportedType(type)
            || maxLevel <= 0)
        {
            return false;
        }

        if (TryFindEquippedSkill(
                normalizedSkillId,
                out SkillType equippedType,
                out SlotIndex slotIndex,
                out int currentLevel))
        {
            return equippedType == type
                && slotIndex != SlotIndex.None
                && currentLevel < maxLevel;
        }

        return type == SkillType.Active
            ? IsSlotEmpty(activeSlot1SkillId, activeSlot1Level)
                || IsSlotEmpty(activeSlot2SkillId, activeSlot2Level)
            : IsSlotEmpty(passiveSlot1SkillId, passiveSlot1Level);
    }

    private bool AcquireOrUpgradeInternal(
        string skillId,
        SkillType type,
        int maxLevel)
    {
        if (!TryNormalizeSkillId(skillId, out string normalizedSkillId)
            || !IsSupportedType(type)
            || maxLevel <= 0)
        {
            return false;
        }

        if (TryFindEquippedSkill(
                normalizedSkillId,
                out SkillType equippedType,
                out SlotIndex slotIndex,
                out int currentLevel))
        {
            if (equippedType != type
                || currentLevel >= maxLevel)
            {
                return false;
            }

            UpgradeSlot(slotIndex);
            return true;
        }

        if (type == SkillType.Active)
        {
            if (IsSlotEmpty(activeSlot1SkillId, activeSlot1Level))
            {
                SetSlot(
                    ref activeSlot1SkillId,
                    ref activeSlot1Level,
                    normalizedSkillId);
                return true;
            }

            if (IsSlotEmpty(activeSlot2SkillId, activeSlot2Level))
            {
                SetSlot(
                    ref activeSlot2SkillId,
                    ref activeSlot2Level,
                    normalizedSkillId);
                return true;
            }

            return false;
        }

        if (!IsSlotEmpty(passiveSlot1SkillId, passiveSlot1Level))
        {
            return false;
        }

        SetSlot(
            ref passiveSlot1SkillId,
            ref passiveSlot1Level,
            normalizedSkillId);
        return true;
    }

    [ContextMenu("Debug Acquire Active A")]
    private void DebugAcquireActiveA()
    {
        RunDebugAcquire(DebugActiveAId, SkillType.Active);
    }

    [ContextMenu("Debug Acquire Active B")]
    private void DebugAcquireActiveB()
    {
        RunDebugAcquire(DebugActiveBId, SkillType.Active);
    }

    [ContextMenu("Debug Acquire Active C")]
    private void DebugAcquireActiveC()
    {
        RunDebugAcquire(DebugActiveCId, SkillType.Active);
    }

    [ContextMenu("Debug Acquire Passive A")]
    private void DebugAcquirePassiveA()
    {
        RunDebugAcquire(DebugPassiveAId, SkillType.Passive);
    }

    [ContextMenu("Debug Acquire Passive B")]
    private void DebugAcquirePassiveB()
    {
        RunDebugAcquire(DebugPassiveBId, SkillType.Passive);
    }

    [ContextMenu("Debug Acquire Magic Missile")]
    private void DebugAcquireMagicMissile()
    {
        RunDebugAcquireDefinition("magic-missile");
    }

    [ContextMenu("Debug Acquire Magic Bolt")]
    private void DebugAcquireMagicBolt()
    {
        RunDebugAcquireDefinition("magic-bolt");
    }

    [ContextMenu("Debug Acquire Fireball")]
    private void DebugAcquireFireball()
    {
        RunDebugAcquireDefinition("fireball");
    }

    [ContextMenu("Debug Acquire Fire Zone")]
    private void DebugAcquireFireZone()
    {
        RunDebugAcquireDefinition("fire-zone");
    }

    [ContextMenu("Debug Acquire Arcane Mastery")]
    private void DebugAcquireArcaneMastery()
    {
        RunDebugAcquireDefinition("arcane-mastery");
    }

    [ContextMenu("Debug Acquire Fire Mastery")]
    private void DebugAcquireFireMastery()
    {
        RunDebugAcquireDefinition("fire-mastery");
    }

    [ContextMenu("Debug Reset Loadout")]
    private void DebugResetLoadout()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Reset Loadout is available only in Play Mode.",
                this);
            return;
        }

        ResetLoadoutState();
        Debug.Log("SkillLoadout debug state reset to empty.", this);
    }

    [ContextMenu("Debug Log School Points")]
    private void DebugLogSchoolPoints()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Log School Points is available only in Play Mode.",
                this);
            return;
        }

        Debug.Log(
            $"Arcane: {GetSchoolPoints(SkillSchool.Arcane)} | "
            + $"Fire: {GetSchoolPoints(SkillSchool.Fire)} | "
            + $"Lightning: {GetSchoolPoints(SkillSchool.Lightning)} | "
            + $"Frost: {GetSchoolPoints(SkillSchool.Frost)}",
            this);
    }

    private void RunDebugAcquire(string skillId, SkillType type)
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "SkillLoadout debug acquisition is available only in Play Mode.",
                this);
            return;
        }

        bool succeeded = AcquireOrUpgrade(skillId, type);
        string result = succeeded ? "succeeded" : "was rejected";
        Debug.Log(
            $"SkillLoadout debug acquisition for '{skillId}' {result}.",
            this);
    }

    private void RunDebugAcquireDefinition(string skillId)
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "SkillLoadout definition debug acquisition is available only in Play Mode.",
                this);
            return;
        }

        if (!SkillCatalog.TryGet(skillId, out SkillDefinition definition))
        {
            Debug.LogWarning(
                $"SkillLoadout could not find debug Skill Definition '{skillId}'.",
                this);
            return;
        }

        bool succeeded = AcquireOrUpgrade(definition);
        string result = succeeded ? "succeeded" : "was rejected";
        Debug.Log(
            $"SkillLoadout definition acquisition for '{definition.Id}' {result}.",
            this);
    }

    private bool TryFindEquippedSkill(
        string skillId,
        out SkillType type,
        out SlotIndex slotIndex,
        out int currentLevel)
    {
        if (IsMatchingOccupiedSlot(
                activeSlot1SkillId,
                activeSlot1Level,
                skillId))
        {
            type = SkillType.Active;
            slotIndex = SlotIndex.Active1;
            currentLevel = activeSlot1Level;
            return true;
        }

        if (IsMatchingOccupiedSlot(
                activeSlot2SkillId,
                activeSlot2Level,
                skillId))
        {
            type = SkillType.Active;
            slotIndex = SlotIndex.Active2;
            currentLevel = activeSlot2Level;
            return true;
        }

        if (IsMatchingOccupiedSlot(
                passiveSlot1SkillId,
                passiveSlot1Level,
                skillId))
        {
            type = SkillType.Passive;
            slotIndex = SlotIndex.Passive1;
            currentLevel = passiveSlot1Level;
            return true;
        }

        type = default;
        slotIndex = SlotIndex.None;
        currentLevel = 0;
        return false;
    }

    private void UpgradeSlot(SlotIndex slotIndex)
    {
        switch (slotIndex)
        {
            case SlotIndex.Active1:
                activeSlot1Level++;
                break;

            case SlotIndex.Active2:
                activeSlot2Level++;
                break;

            case SlotIndex.Passive1:
                passiveSlot1Level++;
                break;
        }
    }

    private void ResetLoadoutState()
    {
        activeSlot1SkillId = string.Empty;
        activeSlot1Level = 0;
        activeSlot2SkillId = string.Empty;
        activeSlot2Level = 0;
        passiveSlot1SkillId = string.Empty;
        passiveSlot1Level = 0;
    }

    private static void SetSlot(
        ref string slotSkillId,
        ref int slotLevel,
        string skillId)
    {
        slotSkillId = skillId;
        slotLevel = 1;
    }

    private static bool TryNormalizeSkillId(
        string skillId,
        out string normalizedSkillId)
    {
        normalizedSkillId = skillId?.Trim();
        return !string.IsNullOrEmpty(normalizedSkillId);
    }

    private static bool IsSupportedType(SkillType type)
    {
        return type == SkillType.Active || type == SkillType.Passive;
    }

    private static bool IsSupportedSchool(SkillSchool school)
    {
        return school == SkillSchool.Arcane
            || school == SkillSchool.Fire
            || school == SkillSchool.Lightning
            || school == SkillSchool.Frost;
    }

    private static int GetSlotSchoolPoints(
        string skillId,
        int level,
        SkillSchool school)
    {
        if (string.IsNullOrEmpty(skillId)
            || level <= 0
            || !SkillCatalog.TryGet(
                skillId,
                out SkillDefinition definition)
            || definition.School != school)
        {
            return 0;
        }

        return level;
    }

    private static bool IsSlotEmpty(string skillId, int level)
    {
        return string.IsNullOrEmpty(skillId) || level <= 0;
    }

    private static bool IsMatchingOccupiedSlot(
        string slotSkillId,
        int slotLevel,
        string skillId)
    {
        return !IsSlotEmpty(slotSkillId, slotLevel)
            && string.Equals(
                slotSkillId,
                skillId,
                StringComparison.Ordinal);
    }
}
