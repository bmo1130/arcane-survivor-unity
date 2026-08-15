using System.Collections.Generic;
using UnityEngine;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private SlimeController slimePrefab;

    [SerializeField]
    private ExperienceOrb experienceOrbPrefab;

    [SerializeField]
    private Transform billboardCamera;

    [SerializeField]
    private RunController runController;

    [Header("Pacing")]
    [SerializeField, Min(0.0001f)]
    private float pacingRampDuration = 240f;

    [SerializeField, Min(0)]
    private int startEnemyCap = 25;

    [SerializeField, Min(0)]
    private int endEnemyCap = 100;

    [SerializeField, Min(0.0001f)]
    private float startSpawnInterval = 1.2f;

    [SerializeField, Min(0.0001f)]
    private float endSpawnInterval = 0.4f;

    [SerializeField, Min(0f)]
    private float spawnDistance = 14f;

    private readonly List<SlimeController> spawnedEnemies = new();

    private PlayerHealth playerHealth;
    private PlayerExperience playerExperience;
    private float spawnTimer;
    private bool spawningEnabled;

    public IReadOnlyList<SlimeController> SpawnedEnemies => spawnedEnemies;
    public Transform BillboardCamera => billboardCamera;
    public bool SpawningEnabled => spawningEnabled;
    public int CurrentEnemyCap => GetEffectiveEnemyCap();
    public float CurrentSpawnInterval => GetEffectiveSpawnInterval();

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError(
                "EnemySpawner requires a Player Transform.",
                this);
            enabled = false;
            return;
        }

        if (slimePrefab == null)
        {
            Debug.LogError(
                "EnemySpawner requires a Slime Prefab.",
                this);
            enabled = false;
            return;
        }

        if (experienceOrbPrefab == null)
        {
            Debug.LogError(
                "EnemySpawner requires an Experience Orb Prefab.",
                this);
            enabled = false;
            return;
        }

        if (billboardCamera == null)
        {
            Debug.LogError(
                "EnemySpawner requires a Billboard Camera Transform.",
                this);
            enabled = false;
            return;
        }

        if (runController == null)
        {
            Debug.LogError(
                "EnemySpawner requires a RunController reference.",
                this);
            enabled = false;
            return;
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        playerExperience = player.GetComponent<PlayerExperience>();

        if (playerHealth == null)
        {
            Debug.LogError(
                "EnemySpawner requires PlayerHealth on the Player GameObject.",
                this);
            enabled = false;
            return;
        }

        if (playerExperience == null)
        {
            Debug.LogError(
                "EnemySpawner requires PlayerExperience on the Player GameObject.",
                this);
            enabled = false;
            return;
        }

        spawnTimer = GetEffectiveSpawnInterval();
        spawningEnabled = true;
    }

    private void Update()
    {
        RemoveDestroyedEnemies();

        if (Time.timeScale <= 0f)
        {
            return;
        }

        if (!spawningEnabled)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;

        if (spawnTimer > 0f)
        {
            return;
        }

        if (spawnedEnemies.Count >= GetEffectiveEnemyCap())
        {
            return;
        }

        SpawnSlime();
        spawnTimer = GetEffectiveSpawnInterval();
    }

    private void SpawnSlime()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Mathf.Max(0f, spawnDistance);
        Vector3 spawnPosition = new Vector3(
            player.position.x + Mathf.Cos(angle) * distance,
            0f,
            player.position.z + Mathf.Sin(angle) * distance);

        SlimeController slime = Instantiate(
            slimePrefab,
            spawnPosition,
            Quaternion.identity);

        TryRegisterSpawnedSlime(slime, false);
    }

    public void StopSpawning()
    {
        spawningEnabled = false;
    }

    public bool TrySpawnBoss(
        SlimeController bossPrefab,
        float distanceFromPlayer,
        out SlimeController boss)
    {
        boss = null;

        if (bossPrefab == null
            || player == null
            || playerHealth == null
            || playerExperience == null
            || experienceOrbPrefab == null
            || billboardCamera == null
            || float.IsNaN(distanceFromPlayer)
            || float.IsInfinity(distanceFromPlayer)
            || distanceFromPlayer < 0f)
        {
            Debug.LogError(
                "EnemySpawner could not spawn the Boss because a required reference or value is invalid.",
                this);
            return false;
        }

        StopSpawning();
        RemoveDestroyedEnemies();

        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector3 spawnPosition = new Vector3(
            player.position.x + Mathf.Cos(angle) * distanceFromPlayer,
            0f,
            player.position.z + Mathf.Sin(angle) * distanceFromPlayer);
        SlimeController spawnedBoss = Instantiate(
            bossPrefab,
            spawnPosition,
            Quaternion.identity);

        if (!TryRegisterSpawnedSlime(spawnedBoss, true))
        {
            return false;
        }

        boss = spawnedBoss;
        return true;
    }

    private bool TryRegisterSpawnedSlime(
        SlimeController slime,
        bool isBoss)
    {
        if (slime == null
            || !slime.Setup(
                player,
                playerHealth,
                spawnedEnemies,
                experienceOrbPrefab,
                playerExperience,
                billboardCamera,
                isBoss))
        {
            if (slime != null)
            {
                Destroy(slime.gameObject);
            }

            return false;
        }

        BillboardToCamera[] billboards =
            slime.GetComponentsInChildren<BillboardToCamera>(true);

        if (billboards.Length == 0)
        {
            Debug.LogError(
                "Spawned Slime requires a BillboardToCamera component in its hierarchy.",
                slime);
            Destroy(slime.gameObject);
            return false;
        }

        foreach (BillboardToCamera billboard in billboards)
        {
            billboard.SetCamera(billboardCamera);
        }

        spawnedEnemies.Add(slime);
        return true;
    }

    private void RemoveDestroyedEnemies()
    {
        for (int index = spawnedEnemies.Count - 1; index >= 0; index--)
        {
            if (spawnedEnemies[index] == null)
            {
                spawnedEnemies.RemoveAt(index);
            }
        }
    }

    private float GetPacingProgress()
    {
        if (runController == null)
        {
            return 0f;
        }

        float safeDuration = Mathf.Max(0.0001f, pacingRampDuration);
        return Mathf.Clamp01(
            runController.ElapsedGameplayTime / safeDuration);
    }

    private int GetEffectiveEnemyCap()
    {
        int safeStartCap = Mathf.Max(0, startEnemyCap);
        int safeEndCap = Mathf.Max(0, endEnemyCap);

        float interpolatedCap = Mathf.Lerp(
            safeStartCap,
            safeEndCap,
            GetPacingProgress());

        return Mathf.FloorToInt(interpolatedCap + 0.5f);
    }

    private float GetEffectiveSpawnInterval()
    {
        float safeStartInterval = Mathf.Max(
            0.0001f,
            startSpawnInterval);
        float safeEndInterval = Mathf.Max(
            0.0001f,
            endSpawnInterval);

        return Mathf.Lerp(
            safeStartInterval,
            safeEndInterval,
            GetPacingProgress());
    }

    [ContextMenu("Debug Log Pacing")]
    private void DebugLogPacing()
    {
        RemoveDestroyedEnemies();

        float elapsed = runController != null
            ? runController.ElapsedGameplayTime
            : 0f;

        Debug.Log(
            $"Elapsed: {elapsed:0} sec | "
            + $"Enemy Cap: {GetEffectiveEnemyCap()} | "
            + $"Spawn Interval: {GetEffectiveSpawnInterval():0.00} | "
            + $"Alive Enemies: {spawnedEnemies.Count}",
            this);
    }
}
