using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SlimeController))]
public sealed class BurnStatus : MonoBehaviour
{
    [SerializeField]
    private bool isBurning;

    [SerializeField, Min(0f)]
    private float remainingDuration;

    [SerializeField, Min(0f)]
    private float currentTickDamage;

    [SerializeField, Min(0f)]
    private float currentTickInterval;

    [SerializeField, Min(0f)]
    private float tickProgress;

    [SerializeField, Min(0f)]
    private float spreadProgress;

    private SlimeController slime;
    private EnemySpawner enemySpawner;
    private PlayerMagicPower playerMagicPower;
    private SkillLoadout skillLoadout;
    private float baseTickDamage;
    private float baseDuration;
    private float baseTickInterval;

    public bool IsBurning => isBurning;
    public float RemainingDuration => remainingDuration;

    private void Awake()
    {
        slime = GetComponent<SlimeController>();

        if (slime == null)
        {
            Debug.LogError(
                "BurnStatus requires SlimeController on the same GameObject.",
                this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || !isBurning
            || slime == null)
        {
            return;
        }

        if (!slime.IsAlive)
        {
            ClearBurn();
            return;
        }

        if (enemySpawner == null
            || playerMagicPower == null
            || skillLoadout == null)
        {
            ClearBurn();
            return;
        }

        float deltaTime = Time.deltaTime;
        remainingDuration = Mathf.Max(0f, remainingDuration - deltaTime);
        tickProgress += deltaTime;

        while (isBurning
            && slime.IsAlive
            && tickProgress >= currentTickInterval)
        {
            tickProgress -= currentTickInterval;
            RefreshCurrentTickDamage();
            slime.TakeDamage(currentTickDamage);
        }

        if (isBurning
            && slime.IsAlive
            && SchoolSynergyUtility.IsFireSpreadActive(skillLoadout))
        {
            spreadProgress += deltaTime;

            while (spreadProgress
                >= SchoolSynergyUtility.FireSpreadInterval)
            {
                spreadProgress -= SchoolSynergyUtility.FireSpreadInterval;
                SpreadBurn();
            }
        }

        if (remainingDuration <= 0f || !slime.IsAlive)
        {
            ClearBurn();
        }
    }

    public void ApplyBurn(
        float newBaseTickDamage,
        float newBaseDuration,
        float newBaseTickInterval,
        EnemySpawner newEnemySpawner,
        PlayerMagicPower newPlayerMagicPower,
        SkillLoadout newSkillLoadout)
    {
        if (!IsPositiveFinite(newBaseTickDamage)
            || !IsPositiveFinite(newBaseDuration)
            || !IsPositiveFinite(newBaseTickInterval)
            || newEnemySpawner == null
            || newPlayerMagicPower == null
            || newSkillLoadout == null
            || slime == null
            || !slime.IsAlive)
        {
            return;
        }

        bool wasBurning = isBurning;
        baseTickDamage = newBaseTickDamage;
        baseDuration = newBaseDuration;
        baseTickInterval = newBaseTickInterval;
        enemySpawner = newEnemySpawner;
        playerMagicPower = newPlayerMagicPower;
        skillLoadout = newSkillLoadout;
        isBurning = true;
        remainingDuration = SchoolSynergyUtility.GetFireBurnDuration(
            skillLoadout,
            baseDuration);
        RefreshCurrentTickDamage();
        currentTickInterval = SchoolSynergyUtility
            .GetFireBurnTickInterval(
                skillLoadout,
                baseTickInterval);

        if (!wasBurning)
        {
            tickProgress = 0f;
            spreadProgress = 0f;
        }
    }

    private void SpreadBurn()
    {
        if (enemySpawner == null
            || playerMagicPower == null
            || skillLoadout == null)
        {
            return;
        }

        float radiusSquared = SchoolSynergyUtility.FireSpreadRadius
            * SchoolSynergyUtility.FireSpreadRadius;
        Vector3 sourcePosition = transform.position;

        foreach (SlimeController enemy in enemySpawner.SpawnedEnemies)
        {
            if (enemy == null
                || enemy == slime
                || !enemy.IsAlive
                || !enemy.TryGetComponent(out BurnStatus burnStatus)
                || burnStatus.IsBurning)
            {
                continue;
            }

            Vector3 toEnemy = enemy.transform.position - sourcePosition;
            toEnemy.y = 0f;

            if (toEnemy.sqrMagnitude > radiusSquared)
            {
                continue;
            }

            burnStatus.ApplyBurn(
                baseTickDamage,
                baseDuration,
                baseTickInterval,
                enemySpawner,
                playerMagicPower,
                skillLoadout);
        }
    }

    private float GetFireMasteryBurnBonus()
    {
        int masteryLevel = skillLoadout.GetSkillLevel("fire-mastery");

        if (masteryLevel >= 2)
        {
            return 3f;
        }

        return masteryLevel >= 1 ? 1f : 0f;
    }

    private void RefreshCurrentTickDamage()
    {
        currentTickDamage = SchoolSynergyUtility.GetModifiedMagicDamage(
            skillLoadout,
            playerMagicPower,
            baseTickDamage + GetFireMasteryBurnBonus());
    }

    private void ClearBurn()
    {
        isBurning = false;
        remainingDuration = 0f;
        currentTickDamage = 0f;
        currentTickInterval = 0f;
        tickProgress = 0f;
        spreadProgress = 0f;
        baseTickDamage = 0f;
        baseDuration = 0f;
        baseTickInterval = 0f;
        enemySpawner = null;
        playerMagicPower = null;
        skillLoadout = null;
    }

    private static bool IsPositiveFinite(float value)
    {
        return !float.IsNaN(value)
            && !float.IsInfinity(value)
            && value > 0f;
    }
}
