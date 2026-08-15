using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerExperience : MonoBehaviour
{
    [SerializeField, Min(1)]
    private int level = 1;

    [SerializeField, Min(0f)]
    private float currentExperience;

    [SerializeField, Min(0.0001f)]
    private float baseExperienceToLevel = 8f;

    [SerializeField, Min(0f)]
    private float experienceGrowthPerLevel = 3f;

    [SerializeField, Min(0f)]
    private float experienceAccelerationPerLevel = 0.5f;

    [SerializeField, Min(0f)]
    private float experienceToNextLevel = 8f;

    public int Level => level;
    public float CurrentExperience => currentExperience;
    public float ExperienceToNextLevel => experienceToNextLevel;

    public event Action<int> LevelsGained;

    private void Awake()
    {
        level = 1;
        currentExperience = 0f;
        baseExperienceToLevel = Mathf.Max(
            0.0001f,
            baseExperienceToLevel);
        experienceGrowthPerLevel = Mathf.Max(
            0f,
            experienceGrowthPerLevel);
        experienceAccelerationPerLevel = Mathf.Max(
            0f,
            experienceAccelerationPerLevel);
        experienceToNextLevel = CalculateExperienceToNextLevel();
    }

    public void AddExperience(float amount)
    {
        if (float.IsNaN(amount)
            || float.IsInfinity(amount)
            || amount <= 0f)
        {
            return;
        }

        float updatedExperience = currentExperience + amount;
        currentExperience = float.IsInfinity(updatedExperience)
            ? float.MaxValue
            : updatedExperience;

        int gainedLevels = 0;

        while (currentExperience >= experienceToNextLevel
            && level < int.MaxValue)
        {
            currentExperience -= experienceToNextLevel;
            level++;
            gainedLevels++;
            experienceToNextLevel = CalculateExperienceToNextLevel();
        }

        if (gainedLevels > 0)
        {
            LevelsGained?.Invoke(gainedLevels);
        }
    }

    private float CalculateExperienceToNextLevel()
    {
        double levelOffset = level - 1d;
        double requirement = baseExperienceToLevel
            + levelOffset * experienceGrowthPerLevel
            + levelOffset
                * levelOffset
                * experienceAccelerationPerLevel;
        double roundedRequirement = Math.Ceiling(requirement);

        return roundedRequirement >= float.MaxValue
            ? float.MaxValue
            : Mathf.Max(0.0001f, (float)roundedRequirement);
    }

    [ContextMenu("Debug Log Experience")]
    private void DebugLogExperience()
    {
        Debug.Log(
            $"Level: {level} | XP: {currentExperience:0.##} / "
            + $"{experienceToNextLevel:0.##}",
            this);
    }
}
