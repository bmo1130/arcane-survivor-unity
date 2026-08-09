using UnityEngine;

public sealed class MagicMissileProjectile : MonoBehaviour
{
    private SlimeController target;
    private BillboardToCamera[] billboards;
    private float damage;
    private float speed;
    private float lifetime;
    private float collisionRadius;
    private float age;
    private float fixedHeight;
    private bool isInitialized;
    private bool isFinished;

    private void Awake()
    {
        billboards = GetComponentsInChildren<BillboardToCamera>(true);
        fixedHeight = transform.position.y;
    }

    public bool Setup(
        SlimeController newTarget,
        float newDamage,
        float newSpeed,
        float newLifetime,
        float newCollisionRadius,
        Transform billboardCamera)
    {
        if (newTarget == null || !newTarget.IsAlive)
        {
            Debug.LogError(
                "MagicMissileProjectile requires a living Slime target.",
                this);
            return false;
        }

        if (billboardCamera == null)
        {
            Debug.LogError(
                "MagicMissileProjectile requires a Billboard Camera Transform.",
                this);
            return false;
        }

        if (billboards.Length == 0)
        {
            Debug.LogError(
                "MagicMissileProjectile requires BillboardToCamera in its hierarchy.",
                this);
            return false;
        }

        target = newTarget;
        damage = Mathf.Max(0f, newDamage);
        speed = Mathf.Max(0f, newSpeed);
        lifetime = Mathf.Max(0f, newLifetime);
        collisionRadius = Mathf.Max(0f, newCollisionRadius);
        fixedHeight = transform.position.y;

        foreach (BillboardToCamera billboard in billboards)
        {
            billboard.SetCamera(billboardCamera);
        }

        isInitialized = true;
        return true;
    }

    private void Update()
    {
        if (!isInitialized || isFinished)
        {
            return;
        }

        age += Time.deltaTime;

        if (age >= lifetime || target == null || !target.IsAlive)
        {
            Finish();
            return;
        }

        Vector3 toTarget = target.transform.position - transform.position;
        toTarget.y = 0f;
        float collisionRadiusSquared = collisionRadius * collisionRadius;

        if (toTarget.sqrMagnitude <= collisionRadiusSquared)
        {
            HitTarget();
            return;
        }

        float distance = toTarget.magnitude;

        if (distance > 0.0001f)
        {
            float moveDistance = Mathf.Min(
                speed * Time.deltaTime,
                distance);
            Vector3 position = transform.position
                + toTarget / distance * moveDistance;
            position.y = fixedHeight;
            transform.position = position;
        }

        Vector3 remainingToTarget = target.transform.position
            - transform.position;
        remainingToTarget.y = 0f;

        if (remainingToTarget.sqrMagnitude <= collisionRadiusSquared)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        if (target != null && target.IsAlive)
        {
            target.TakeDamage(damage);
        }

        Finish();
    }

    private void Finish()
    {
        if (isFinished)
        {
            return;
        }

        isFinished = true;
        enabled = false;
        Destroy(gameObject);
    }
}
