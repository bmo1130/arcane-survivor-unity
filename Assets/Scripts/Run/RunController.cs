using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class RunController : MonoBehaviour
{
    public enum RunState
    {
        WaitingForStartingSpell,
        Running,
        Boss,
        Victory,
        Defeat
    }

    [SerializeField]
    private StartingSpellSelectionController startingSpellSelectionController;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private SlimeController bossSlimePrefab;

    [SerializeField]
    private RunTimerUI runTimerUI;

    [Header("Run End UI")]
    [SerializeField]
    private GameObject runEndPanel;

    [SerializeField]
    private Text runEndTitleText;

    [SerializeField]
    private Text runEndSubtitleText;

    [Header("Run")]
    [SerializeField, Min(0f)]
    private float runDuration = 300f;

    [SerializeField, Min(0f)]
    private float bossSpawnDistance = 11f;

    [SerializeField]
    private RunState state = RunState.WaitingForStartingSpell;

    [SerializeField, Min(0f)]
    private float elapsedGameplayTime;

    private SlimeController activeBoss;
    private bool ownsRunEndPause;

    public RunState State => state;
    public float ElapsedGameplayTime => elapsedGameplayTime;
    public bool IsRunEnded => state == RunState.Victory
        || state == RunState.Defeat;

    private void Awake()
    {
        state = RunState.WaitingForStartingSpell;
        elapsedGameplayTime = 0f;
        ownsRunEndPause = false;

        if (runEndPanel != null)
        {
            runEndPanel.SetActive(false);
        }

        if (!ValidateReferences())
        {
            enabled = false;
            return;
        }

        runTimerUI.SetTime(0f, false);
    }

    private void OnEnable()
    {
        if (playerHealth != null)
        {
            playerHealth.Died -= HandlePlayerDied;
            playerHealth.Died += HandlePlayerDied;
        }

        SubscribeToBoss(activeBoss);
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.Died -= HandlePlayerDied;
        }

        UnsubscribeFromBoss(activeBoss);

        if (ownsRunEndPause)
        {
            Time.timeScale = 1f;
            ownsRunEndPause = false;
        }
    }

    private void Update()
    {
        if (IsRunEnded)
        {
            return;
        }

        if (playerHealth.IsDead)
        {
            EndRun(false);
            return;
        }

        if (state == RunState.WaitingForStartingSpell)
        {
            if (!startingSpellSelectionController.SelectionCompleted)
            {
                return;
            }

            state = RunState.Running;
        }

        if (state == RunState.Running)
        {
            float safeDuration = Mathf.Max(0f, runDuration);
            elapsedGameplayTime = Mathf.Min(
                safeDuration,
                elapsedGameplayTime + Time.deltaTime);

            if (elapsedGameplayTime >= safeDuration)
            {
                BeginBossPhase();
            }
        }

        runTimerUI.SetTime(
            elapsedGameplayTime,
            state == RunState.Boss);
    }

    [ContextMenu("Debug Spawn Boss Now")]
    private void DebugSpawnBossNow()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Spawn Boss Now is available only in Play Mode.",
                this);
            return;
        }

        if (IsRunEnded || state == RunState.Boss)
        {
            return;
        }

        if (!startingSpellSelectionController.SelectionCompleted)
        {
            Debug.LogWarning(
                "Choose a Starting Spell before spawning the Boss.",
                this);
            return;
        }

        state = RunState.Running;
        elapsedGameplayTime = Mathf.Max(0f, runDuration);
        BeginBossPhase();
        runTimerUI.SetTime(elapsedGameplayTime, state == RunState.Boss);
    }

    // Temporary Editor verification hook that uses the real Boss damage/death path.
    [ContextMenu("Debug Defeat Active Boss")]
    private void DebugDefeatActiveBoss()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning(
                "Debug Defeat Active Boss is available only in Play Mode.",
                this);
            return;
        }

        if (state != RunState.Boss
            || activeBoss == null
            || !activeBoss.IsAlive)
        {
            Debug.LogWarning(
                "There is no active living Boss to defeat.",
                this);
            return;
        }

        activeBoss.TakeDamage(activeBoss.CurrentHealth);
    }

    private void BeginBossPhase()
    {
        if (state != RunState.Running || IsRunEnded)
        {
            return;
        }

        state = RunState.Boss;
        enemySpawner.StopSpawning();

        if (!enemySpawner.TrySpawnBoss(
                bossSlimePrefab,
                Mathf.Max(0f, bossSpawnDistance),
                out SlimeController spawnedBoss))
        {
            Debug.LogError(
                "RunController could not spawn the Boss Slime.",
                this);
            return;
        }

        activeBoss = spawnedBoss;
        SubscribeToBoss(activeBoss);
    }

    private void SubscribeToBoss(SlimeController boss)
    {
        if (boss == null)
        {
            return;
        }

        boss.Died -= HandleBossDied;
        boss.Died += HandleBossDied;
    }

    private void UnsubscribeFromBoss(SlimeController boss)
    {
        if (boss != null)
        {
            boss.Died -= HandleBossDied;
        }
    }

    private void HandleBossDied(SlimeController defeatedBoss)
    {
        if (state != RunState.Boss
            || defeatedBoss == null
            || defeatedBoss != activeBoss)
        {
            return;
        }

        UnsubscribeFromBoss(activeBoss);
        activeBoss = null;
        EndRun(true);
    }

    private void HandlePlayerDied()
    {
        EndRun(false);
    }

    private void EndRun(bool victory)
    {
        if (IsRunEnded)
        {
            return;
        }

        state = victory ? RunState.Victory : RunState.Defeat;
        enemySpawner.StopSpawning();
        runEndTitleText.text = victory ? "VICTORY" : "DEFEAT";
        runEndSubtitleText.text = victory
            ? "Boss Defeated"
            : "The run has ended";
        runEndPanel.SetActive(true);
        Time.timeScale = 0f;
        ownsRunEndPause = true;
    }

    private bool ValidateReferences()
    {
        if (startingSpellSelectionController == null
            || enemySpawner == null
            || playerHealth == null
            || bossSlimePrefab == null
            || runTimerUI == null
            || !runTimerUI.IsConfigured
            || runEndPanel == null
            || runEndTitleText == null
            || runEndSubtitleText == null)
        {
            Debug.LogError(
                "RunController requires Starting Spell Selection, EnemySpawner, PlayerHealth, Boss Prefab, Timer UI, and Run End UI references.",
                this);
            return false;
        }

        if (runEndTitleText == runEndSubtitleText
            || float.IsNaN(runDuration)
            || float.IsInfinity(runDuration)
            || runDuration < 0f
            || float.IsNaN(bossSpawnDistance)
            || float.IsInfinity(bossSpawnDistance)
            || bossSpawnDistance <= 0f)
        {
            Debug.LogError(
                "RunController requires different Run End Text references and valid Run values.",
                this);
            return false;
        }

        return true;
    }
}
