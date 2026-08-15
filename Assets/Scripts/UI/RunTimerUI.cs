using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public sealed class RunTimerUI : MonoBehaviour
{
    [SerializeField]
    private Text runTimerText;

    public bool IsConfigured => runTimerText != null;

    private void Awake()
    {
        if (runTimerText == null)
        {
            Debug.LogError(
                "RunTimerUI requires a Legacy Text reference.",
                this);
            enabled = false;
            return;
        }

        SetTime(0f, false);
    }

    public void SetTime(float elapsedSeconds, bool bossPhase)
    {
        if (runTimerText == null)
        {
            return;
        }

        float safeSeconds = float.IsNaN(elapsedSeconds)
            || float.IsInfinity(elapsedSeconds)
            ? 0f
            : Mathf.Max(0f, elapsedSeconds);
        int totalSeconds = Mathf.FloorToInt(safeSeconds);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        string bossSuffix = bossPhase ? "  BOSS" : string.Empty;
        runTimerText.text = $"{minutes:00}:{seconds:00}{bossSuffix}";
    }
}
