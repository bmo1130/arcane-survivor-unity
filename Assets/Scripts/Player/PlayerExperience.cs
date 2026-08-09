using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerExperience : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float currentExperience;

    public float CurrentExperience => currentExperience;

    private void Awake()
    {
        currentExperience = 0f;
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
    }
}
