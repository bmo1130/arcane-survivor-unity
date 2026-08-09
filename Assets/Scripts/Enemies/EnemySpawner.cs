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

    [SerializeField, Min(0f)]
    private float spawnInterval = 1.5f;

    [SerializeField, Min(0f)]
    private float spawnDistance = 14f;

    [SerializeField, Min(0)]
    private int maximumEnemyCount = 20;

    private readonly List<SlimeController> spawnedEnemies = new();

    private PlayerHealth playerHealth;
    private PlayerExperience playerExperience;
    private float spawnTimer;

    public IReadOnlyList<SlimeController> SpawnedEnemies => spawnedEnemies;
    public Transform BillboardCamera => billboardCamera;

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

        spawnTimer = Mathf.Max(0f, spawnInterval);
    }

    private void Update()
    {
        RemoveDestroyedEnemies();

        spawnTimer -= Time.deltaTime;

        if (spawnTimer > 0f)
        {
            return;
        }

        spawnTimer = Mathf.Max(0f, spawnInterval);

        int enemyLimit = Mathf.Max(0, maximumEnemyCount);

        if (spawnedEnemies.Count >= enemyLimit)
        {
            return;
        }

        SpawnSlime();
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

        if (!slime.Setup(
                player,
                playerHealth,
                spawnedEnemies,
                experienceOrbPrefab,
                playerExperience,
                billboardCamera))
        {
            Destroy(slime.gameObject);
            return;
        }

        BillboardToCamera[] billboards =
            slime.GetComponentsInChildren<BillboardToCamera>(true);

        if (billboards.Length == 0)
        {
            Debug.LogError(
                "The Slime Prefab requires a BillboardToCamera component in its hierarchy.",
                slime);
            enabled = false;
            Destroy(slime.gameObject);
            return;
        }

        foreach (BillboardToCamera billboard in billboards)
        {
            billboard.SetCamera(billboardCamera);
        }

        spawnedEnemies.Add(slime);
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
}
