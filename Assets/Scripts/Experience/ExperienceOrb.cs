using UnityEngine;

[DisallowMultipleComponent]
public sealed class ExperienceOrb : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float value = 4f;

    [SerializeField, Min(0f)]
    private float pickupRadius = 1.1f;

    private Transform player;
    private PlayerExperience playerExperience;
    private BillboardToCamera[] billboards;
    private bool isInitialized;
    private bool isCollected;

    private void Awake()
    {
        billboards = GetComponentsInChildren<BillboardToCamera>(true);
    }

    private void Start()
    {
        if (isInitialized)
        {
            return;
        }

        Debug.LogError(
            "ExperienceOrb must be initialized through Setup after it is spawned.",
            this);
        enabled = false;
    }

    public bool Setup(
        Transform newPlayer,
        PlayerExperience newPlayerExperience,
        Transform billboardCamera,
        float newValue)
    {
        if (newPlayer == null)
        {
            Debug.LogError(
                "ExperienceOrb requires a Player Transform.",
                this);
            return false;
        }

        if (newPlayerExperience == null)
        {
            Debug.LogError(
                "ExperienceOrb requires a PlayerExperience reference.",
                this);
            return false;
        }

        if (billboardCamera == null)
        {
            Debug.LogError(
                "ExperienceOrb requires a Billboard Camera Transform.",
                this);
            return false;
        }

        if (billboards.Length == 0)
        {
            Debug.LogError(
                "ExperienceOrb requires BillboardToCamera in its hierarchy.",
                this);
            return false;
        }

        player = newPlayer;
        playerExperience = newPlayerExperience;
        value = newValue;

        foreach (BillboardToCamera billboard in billboards)
        {
            billboard.SetCamera(billboardCamera);
        }

        isInitialized = true;
        enabled = true;
        return true;
    }

    private void Update()
    {
        if (Time.timeScale <= 0f
            || !isInitialized
            || isCollected
            || player == null
            || playerExperience == null)
        {
            return;
        }

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0f;

        float radius = Mathf.Max(0f, pickupRadius);

        if (toPlayer.sqrMagnitude <= radius * radius)
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (isCollected || playerExperience == null)
        {
            return;
        }

        isCollected = true;
        enabled = false;
        playerExperience.AddExperience(value);
        Destroy(gameObject);
    }
}
