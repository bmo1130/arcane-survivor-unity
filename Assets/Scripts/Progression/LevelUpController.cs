using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class LevelUpController : MonoBehaviour
{
    private sealed class LevelUpCandidate
    {
        public int CommonChoiceIndex { get; }
        public SkillDefinition Skill { get; }
        public bool IsSchoolSkill => Skill != null;

        public LevelUpCandidate(int commonChoiceIndex)
        {
            CommonChoiceIndex = commonChoiceIndex;
        }

        public LevelUpCandidate(SkillDefinition skill)
        {
            CommonChoiceIndex = -1;
            Skill = skill;
        }
    }

    [SerializeField]
    private PlayerExperience playerExperience;

    [SerializeField]
    private LevelUpChoiceUI levelUpChoiceUI;

    [SerializeField]
    private CommonUpgradeController commonUpgradeController;

    [SerializeField, Min(0)]
    private int pendingLevelUps;

    private bool ownsPause;
    private SkillLoadout skillLoadout;
    private readonly List<LevelUpCandidate> candidatePool = new();
    private readonly List<LevelUpCandidate> currentChoices = new(
        LevelUpChoiceUI.ChoiceCount);

    public int PendingLevelUps => pendingLevelUps;

    private void Awake()
    {
        pendingLevelUps = 0;
        skillLoadout = GetComponent<SkillLoadout>();

        if (playerExperience == null)
        {
            Debug.LogError(
                "LevelUpController requires the Player's PlayerExperience component.",
                this);
            enabled = false;
            return;
        }

        if (commonUpgradeController == null)
        {
            Debug.LogError(
                "LevelUpController requires a CommonUpgradeController.",
                this);
            enabled = false;
            return;
        }

        if (skillLoadout == null)
        {
            Debug.LogError(
                "LevelUpController requires SkillLoadout on the same GameObject.",
                this);
            enabled = false;
        }
    }

    private void Start()
    {
        if (commonUpgradeController == null
            || !commonUpgradeController.enabled)
        {
            Debug.LogError(
                "LevelUpController requires an enabled CommonUpgradeController.",
                this);
            enabled = false;
            return;
        }

        if (levelUpChoiceUI != null
            && levelUpChoiceUI.Initialize(this))
        {
            return;
        }

        Debug.LogError(
            "LevelUpController requires a configured LevelUpChoiceUI.",
            this);
        enabled = false;
    }

    private void OnEnable()
    {
        if (playerExperience == null)
        {
            return;
        }

        playerExperience.LevelsGained += HandleLevelsGained;

        if (pendingLevelUps > 0)
        {
            PauseGameplay();
            ShowNextChoices();
        }
    }

    private void OnDisable()
    {
        if (playerExperience != null)
        {
            playerExperience.LevelsGained -= HandleLevelsGained;
        }

        levelUpChoiceUI?.HideChoices();
        ResumeGameplay();
    }

    private void OnDestroy()
    {
        levelUpChoiceUI?.HideChoices();
        ResumeGameplay();
    }

    private void HandleLevelsGained(int gainedLevels)
    {
        if (gainedLevels <= 0)
        {
            return;
        }

        long updatedPending = (long)pendingLevelUps + gainedLevels;
        pendingLevelUps = updatedPending >= int.MaxValue
            ? int.MaxValue
            : (int)updatedPending;
        PauseGameplay();
        ShowNextChoices();
    }

    public void SelectChoice(int choiceIndex)
    {
        if (choiceIndex < 0
            || choiceIndex >= LevelUpChoiceUI.ChoiceCount
            || pendingLevelUps <= 0
            || levelUpChoiceUI == null
            || !levelUpChoiceUI.IsShowing)
        {
            return;
        }

        if (choiceIndex >= currentChoices.Count
            || !ApplyChoice(currentChoices[choiceIndex]))
        {
            Debug.LogError(
                "LevelUpController could not apply the selected Level-Up choice.",
                this);
            ShowNextChoices();
            return;
        }

        CompletePendingLevelUp();
    }

    public string GetChoiceLabel(int choiceIndex)
    {
        if (choiceIndex < 0 || choiceIndex >= currentChoices.Count)
        {
            return string.Empty;
        }

        LevelUpCandidate candidate = currentChoices[choiceIndex];

        return candidate.IsSchoolSkill
            ? BuildSchoolSkillLabel(candidate.Skill)
            : commonUpgradeController.GetChoiceLabel(
                candidate.CommonChoiceIndex);
    }

    public void CompletePendingLevelUp()
    {
        if (pendingLevelUps <= 0)
        {
            return;
        }

        pendingLevelUps--;

        if (pendingLevelUps > 0)
        {
            PauseGameplay();
            ShowNextChoices();
            return;
        }

        currentChoices.Clear();
        levelUpChoiceUI?.HideChoices();

        if (pendingLevelUps == 0)
        {
            ResumeGameplay();
        }
    }

    // Editor fallback for testing pause/pending flow without applying an upgrade.
    [ContextMenu("Debug Complete Pending Level Up")]
    private void DebugCompletePendingLevelUp()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Complete Pending Level Up is available only in Play Mode.",
                this);
            return;
        }

        CompletePendingLevelUp();
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

    private void ShowNextChoices()
    {
        if (levelUpChoiceUI == null || !BuildCurrentChoices())
        {
            Debug.LogError(
                "LevelUpController could not build three Level-Up choices.",
                this);
            return;
        }

        levelUpChoiceUI.ShowChoices();
    }

    private bool BuildCurrentChoices()
    {
        candidatePool.Clear();
        currentChoices.Clear();

        for (int commonIndex = 0;
            commonIndex < CommonUpgradeController.ChoiceCount;
            commonIndex++)
        {
            candidatePool.Add(new LevelUpCandidate(commonIndex));
        }

        foreach (SkillDefinition skill in SkillCatalog.All)
        {
            if (skillLoadout.CanAcquireOrUpgrade(skill))
            {
                candidatePool.Add(new LevelUpCandidate(skill));
            }
        }

        if (candidatePool.Count < LevelUpChoiceUI.ChoiceCount)
        {
            return false;
        }

        ShuffleCandidatePool();

        for (int index = 0;
            index < LevelUpChoiceUI.ChoiceCount;
            index++)
        {
            currentChoices.Add(candidatePool[index]);
        }

        return true;
    }

    private void ShuffleCandidatePool()
    {
        for (int index = candidatePool.Count - 1; index > 0; index--)
        {
            int randomIndex = Random.Range(0, index + 1);
            LevelUpCandidate temporary = candidatePool[index];
            candidatePool[index] = candidatePool[randomIndex];
            candidatePool[randomIndex] = temporary;
        }
    }

    private bool ApplyChoice(LevelUpCandidate candidate)
    {
        if (candidate == null)
        {
            return false;
        }

        return candidate.IsSchoolSkill
            ? skillLoadout.AcquireOrUpgrade(candidate.Skill)
            : commonUpgradeController.ApplyUpgrade(
                candidate.CommonChoiceIndex);
    }

    private string BuildSchoolSkillLabel(SkillDefinition skill)
    {
        if (skill == null)
        {
            return string.Empty;
        }

        int currentLevel = skillLoadout.GetSkillLevel(skill.Id);
        int nextLevel = Mathf.Min(currentLevel + 1, skill.MaxLevel);
        string description = GetSkillDescription(skill.Id, nextLevel);

        return $"{skill.School} · {skill.Type} · {skill.DisplayName}\n"
            + $"Lv.{currentLevel} → Lv.{nextLevel}\n"
            + description;
    }

    private static string GetSkillDescription(
        string skillId,
        int nextLevel)
    {
        return skillId switch
        {
            "magic-missile" => nextLevel >= 2
                ? "Projectile +1 · Damage +1"
                : "Homing projectile · Damage 3",
            "magic-bolt" => nextLevel >= 2
                ? "Projectile +1 · Damage +1"
                : "Fast straight projectile · Damage 4",
            "arcane-mastery" => nextLevel >= 2
                ? "Total Spell Cooldown Reduction 15%"
                : "All Spell Cooldown -10%",
            "fireball" => nextLevel >= 2
                ? "Explosion Radius increased"
                : "Impact applies Burning in an area",
            "fire-zone" => nextLevel >= 2
                ? "Area Radius increased"
                : "Burning area at enemy position",
            "fire-mastery" => nextLevel >= 2
                ? "Total Burning Damage Bonus +3"
                : "Burning Damage +1",
            "chain-lightning" => nextLevel >= 2
                ? "Base Damage 2"
                : "Damage 1 · +2 Bounces",
            "lightning-orb" => nextLevel >= 2
                ? "Pulse Base Bounce +1"
                : "Periodic nearby lightning attack",
            "lightning-mastery" => nextLevel >= 2
                ? "Total Lightning Bounce Bonus +2"
                : "Lightning Bounce +1",
            "ice-bolt" => nextLevel >= 2
                ? "Small AoE Damage + Slow"
                : "Damage + Slow",
            "blizzard" => nextLevel >= 2
                ? "Area Radius increased"
                : "Persistent Damage + Slow area",
            "frost-mastery" => nextLevel >= 2
                ? "Movement Slow strengthened"
                : "Slow also reduces Attack Speed",
            _ => string.Empty
        };
    }
}
