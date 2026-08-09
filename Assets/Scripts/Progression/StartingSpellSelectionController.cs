using System;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class StartingSpellSelectionController : MonoBehaviour
{
    public const int StartingChoiceCount = 8;

    private static readonly IReadOnlyList<SkillDefinition>
        ReadOnlyStartingChoices = CreateStartingChoices();

    [SerializeField]
    private SkillLoadout skillLoadout;

    [SerializeField]
    private StartingSpellSelectionUI startingSpellSelectionUI;

    [SerializeField]
    private bool awaitingSelection;

    private bool ownsPause;

    public bool AwaitingSelection => awaitingSelection;
    public IReadOnlyList<SkillDefinition> StartingChoices =>
        ReadOnlyStartingChoices;

    private void Awake()
    {
        awaitingSelection = false;
        PauseGameplay();

        if (skillLoadout != null
            && startingSpellSelectionUI != null)
        {
            return;
        }

        Debug.LogError(
            "StartingSpellSelectionController requires SkillLoadout "
            + "and StartingSpellSelectionUI references.",
            this);
        enabled = false;
    }

    private void Start()
    {
        if (!ValidateStartingChoices())
        {
            Debug.LogError(
                $"Starting Spell Selection requires exactly "
                + $"{StartingChoiceCount} Active Skills in SkillCatalog.",
                this);
            enabled = false;
            return;
        }

        if (!skillLoadout.IsEmpty)
        {
            Debug.LogError(
                "Starting Spell Selection requires an empty SkillLoadout at game start.",
                this);
            enabled = false;
            return;
        }

        if (!startingSpellSelectionUI.Initialize(
                this,
                ReadOnlyStartingChoices))
        {
            Debug.LogError(
                "StartingSpellSelectionController requires a configured "
                + "StartingSpellSelectionUI.",
                this);
            enabled = false;
            return;
        }

        awaitingSelection = true;
        startingSpellSelectionUI.ShowChoices();
    }

    public bool TrySelectStartingSpell(string skillId)
    {
        if (!awaitingSelection
            || skillLoadout == null
            || string.IsNullOrWhiteSpace(skillId)
            || !SkillCatalog.TryGet(skillId, out SkillDefinition definition)
            || definition.Type != SkillType.Active
            || !IsStartingChoice(definition)
            || !skillLoadout.IsEmpty)
        {
            return false;
        }

        if (!skillLoadout.AcquireOrUpgrade(definition))
        {
            return false;
        }

        if (!HasExpectedSelectedLoadout(definition))
        {
            Debug.LogError(
                "Starting Spell Selection did not produce the expected "
                + "Active Slot 1 Lv.1 loadout.",
                this);
            return false;
        }

        awaitingSelection = false;
        startingSpellSelectionUI.HideChoices();
        ResumeGameplay();
        return true;
    }

    [ContextMenu("Debug Choose Magic Missile")]
    private void DebugChooseMagicMissile()
    {
        RunDebugSelection("magic-missile");
    }

    [ContextMenu("Debug Choose Magic Bolt")]
    private void DebugChooseMagicBolt()
    {
        RunDebugSelection("magic-bolt");
    }

    [ContextMenu("Debug Choose Fireball")]
    private void DebugChooseFireball()
    {
        RunDebugSelection("fireball");
    }

    [ContextMenu("Debug Choose Fire Zone")]
    private void DebugChooseFireZone()
    {
        RunDebugSelection("fire-zone");
    }

    [ContextMenu("Debug Choose Chain Lightning")]
    private void DebugChooseChainLightning()
    {
        RunDebugSelection("chain-lightning");
    }

    [ContextMenu("Debug Choose Lightning Orb")]
    private void DebugChooseLightningOrb()
    {
        RunDebugSelection("lightning-orb");
    }

    [ContextMenu("Debug Choose Ice Bolt")]
    private void DebugChooseIceBolt()
    {
        RunDebugSelection("ice-bolt");
    }

    [ContextMenu("Debug Choose Blizzard")]
    private void DebugChooseBlizzard()
    {
        RunDebugSelection("blizzard");
    }

    private void RunDebugSelection(string skillId)
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Starting Spell debug selection is available only in Play Mode.",
                this);
            return;
        }

        bool succeeded = TrySelectStartingSpell(skillId);
        string result = succeeded ? "succeeded" : "was rejected";
        Debug.Log(
            $"Starting Spell debug selection for '{skillId}' {result}.",
            this);
    }

    private bool HasExpectedSelectedLoadout(SkillDefinition definition)
    {
        return string.Equals(
                skillLoadout.ActiveSlot1SkillId,
                definition.Id,
                StringComparison.Ordinal)
            && skillLoadout.ActiveSlot1Level == 1
            && string.IsNullOrEmpty(skillLoadout.ActiveSlot2SkillId)
            && skillLoadout.ActiveSlot2Level == 0
            && string.IsNullOrEmpty(skillLoadout.PassiveSlot1SkillId)
            && skillLoadout.PassiveSlot1Level == 0;
    }

    private static bool IsStartingChoice(SkillDefinition definition)
    {
        for (int index = 0; index < ReadOnlyStartingChoices.Count; index++)
        {
            if (ReferenceEquals(ReadOnlyStartingChoices[index], definition))
            {
                return true;
            }
        }

        return false;
    }

    private static IReadOnlyList<SkillDefinition> CreateStartingChoices()
    {
        List<SkillDefinition> startingChoices = new();

        foreach (SkillDefinition definition in SkillCatalog.All)
        {
            if (definition.Type == SkillType.Active)
            {
                startingChoices.Add(definition);
            }
        }

        return startingChoices.AsReadOnly();
    }

    private static bool ValidateStartingChoices()
    {
        if (ReadOnlyStartingChoices.Count != StartingChoiceCount)
        {
            return false;
        }

        for (int index = 0; index < ReadOnlyStartingChoices.Count; index++)
        {
            if (ReadOnlyStartingChoices[index] == null
                || ReadOnlyStartingChoices[index].Type != SkillType.Active)
            {
                return false;
            }
        }

        return true;
    }

    private void PauseGameplay()
    {
        if (ownsPause)
        {
            return;
        }

        Time.timeScale = 0f;
        ownsPause = true;
    }

    private void ResumeGameplay()
    {
        if (!ownsPause)
        {
            return;
        }

        Time.timeScale = 1f;
        ownsPause = false;
    }
}
