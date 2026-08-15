using UnityEngine;

[DisallowMultipleComponent]
public sealed class CommonUpgradeController : MonoBehaviour
{
    public const int MaximumHealthChoiceIndex = 0;
    public const int MagicPowerChoiceIndex = 1;
    public const int RegenerationChoiceIndex = 2;
    public const int ChoiceCount = 3;

    private const float MaximumHealthIncrease = 10f;
    private const float MagicDamageIncrease = 1f;
    private const float RegenerationIncrease = 0.5f;

    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private PlayerMagicPower playerMagicPower;

    [SerializeField, Min(0)]
    private int maximumHealthLevel;

    [SerializeField, Min(0)]
    private int magicPowerLevel;

    [SerializeField, Min(0)]
    private int regenerationLevel;

    public int MaximumHealthLevel => maximumHealthLevel;
    public int MagicPowerLevel => magicPowerLevel;
    public int RegenerationLevel => regenerationLevel;

    private void Awake()
    {
        maximumHealthLevel = 0;
        magicPowerLevel = 0;
        regenerationLevel = 0;

        if (playerHealth == null || playerMagicPower == null)
        {
            Debug.LogError(
                "CommonUpgradeController requires PlayerHealth and PlayerMagicPower references.",
                this);
            enabled = false;
        }
    }

    public bool ApplyUpgrade(int choiceIndex)
    {
        if (!enabled || playerHealth == null || playerMagicPower == null)
        {
            return false;
        }

        switch (choiceIndex)
        {
            case MaximumHealthChoiceIndex:
                playerHealth.IncreaseMaximumHealth(MaximumHealthIncrease);
                maximumHealthLevel = IncrementLevel(maximumHealthLevel);
                return true;

            case MagicPowerChoiceIndex:
                playerMagicPower.IncreaseMagicDamage(MagicDamageIncrease);
                magicPowerLevel = IncrementLevel(magicPowerLevel);
                return true;

            case RegenerationChoiceIndex:
                playerHealth.IncreaseHealthRegeneration(RegenerationIncrease);
                regenerationLevel = IncrementLevel(regenerationLevel);
                return true;

            default:
                return false;
        }
    }

    public string GetChoiceLabel(int choiceIndex)
    {
        switch (choiceIndex)
        {
            case MaximumHealthChoiceIndex:
                return BuildLabel(
                    "Maximum Health",
                    maximumHealthLevel,
                    "Max HP +10 / Current HP +10");

            case MagicPowerChoiceIndex:
                return BuildLabel(
                    "Magic Power",
                    magicPowerLevel,
                    "All Magic Damage +1");

            case RegenerationChoiceIndex:
                return BuildLabel(
                    "Regeneration",
                    regenerationLevel,
                    "HP Regen +0.5/sec");

            default:
                return string.Empty;
        }
    }

    private static string BuildLabel(
        string upgradeName,
        int currentLevel,
        string effectDescription)
    {
        int nextLevel = IncrementLevel(currentLevel);
        return $"Common · {upgradeName}\n"
            + $"Lv.{currentLevel} → Lv.{nextLevel}\n"
            + effectDescription;
    }

    private static int IncrementLevel(int currentLevel)
    {
        return currentLevel < int.MaxValue
            ? currentLevel + 1
            : int.MaxValue;
    }
}
