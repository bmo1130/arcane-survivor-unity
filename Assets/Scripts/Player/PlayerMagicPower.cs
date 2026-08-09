using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerMagicPower : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float magicDamageBonus;

    public float MagicDamageBonus => magicDamageBonus;

    private void Awake()
    {
        magicDamageBonus = 0f;
    }

    public void IncreaseMagicDamage(float amount)
    {
        if (!IsPositiveFinite(amount))
        {
            return;
        }

        double result = (double)magicDamageBonus + amount;
        magicDamageBonus = result >= float.MaxValue
            ? float.MaxValue
            : (float)result;
    }

    public float GetModifiedMagicDamage(float baseDamage)
    {
        if (!IsPositiveFinite(baseDamage))
        {
            return 0f;
        }

        double result = (double)baseDamage + magicDamageBonus;
        return result >= float.MaxValue
            ? float.MaxValue
            : (float)result;
    }

    private static bool IsPositiveFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value > 0f;
    }
}
