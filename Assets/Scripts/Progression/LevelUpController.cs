using UnityEngine;

[DisallowMultipleComponent]
public sealed class LevelUpController : MonoBehaviour
{
    [SerializeField]
    private PlayerExperience playerExperience;

    [SerializeField]
    private LevelUpChoiceUI levelUpChoiceUI;

    [SerializeField, Min(0)]
    private int pendingLevelUps;

    private bool ownsPause;

    public int PendingLevelUps => pendingLevelUps;

    private void Awake()
    {
        pendingLevelUps = 0;

        if (playerExperience != null)
        {
            return;
        }

        Debug.LogError(
            "LevelUpController requires the Player's PlayerExperience component.",
            this);
        enabled = false;
        return;
    }

    private void Start()
    {
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
            levelUpChoiceUI?.ShowChoices();
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
        levelUpChoiceUI?.ShowChoices();
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

        // U6-B validates the UI flow only. Upgrade effects begin in U6-C.
        CompletePendingLevelUp();
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
            levelUpChoiceUI?.ShowChoices();
            return;
        }

        levelUpChoiceUI?.HideChoices();

        if (pendingLevelUps == 0)
        {
            ResumeGameplay();
        }
    }

    // Editor fallback while U6-B verifies the normal Button flow.
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
}
