using UnityEngine;

[DisallowMultipleComponent]
public sealed class LevelUpController : MonoBehaviour
{
    [SerializeField]
    private PlayerExperience playerExperience;

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
        }
    }

    private void OnDisable()
    {
        if (playerExperience != null)
        {
            playerExperience.LevelsGained -= HandleLevelsGained;
        }

        ResumeGameplay();
    }

    private void OnDestroy()
    {
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
    }

    public void CompletePendingLevelUp()
    {
        if (pendingLevelUps <= 0)
        {
            return;
        }

        pendingLevelUps--;

        if (pendingLevelUps == 0)
        {
            ResumeGameplay();
        }
    }

    // Temporary Editor verification hook until U6-B completes choices.
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
