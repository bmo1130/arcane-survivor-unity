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
    public const int MaximumSkillLevel = 2;

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
        if (!TryNormalizeSkillId(skillId, out string normalizedSkillId)
            || !IsSupportedType(type))
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
                && currentLevel < MaximumSkillLevel;
        }

        return type == SkillType.Active
            ? IsSlotEmpty(activeSlot1SkillId, activeSlot1Level)
                || IsSlotEmpty(activeSlot2SkillId, activeSlot2Level)
            : IsSlotEmpty(passiveSlot1SkillId, passiveSlot1Level);
    }

    public bool AcquireOrUpgrade(string skillId, SkillType type)
    {
        if (!TryNormalizeSkillId(skillId, out string normalizedSkillId)
            || !IsSupportedType(type))
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
                || currentLevel >= MaximumSkillLevel)
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
