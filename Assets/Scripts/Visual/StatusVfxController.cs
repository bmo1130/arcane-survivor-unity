using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BurnStatus))]
[RequireComponent(typeof(SlowStatus))]
public sealed class StatusVfxController : MonoBehaviour
{
    [SerializeField]
    private GameObject burnVfx;

    [SerializeField]
    private GameObject slowVfx;

    private BurnStatus burnStatus;
    private SlowStatus slowStatus;

    private void Awake()
    {
        burnStatus = GetComponent<BurnStatus>();
        slowStatus = GetComponent<SlowStatus>();

        if (!ValidateReferences())
        {
            enabled = false;
            return;
        }

        RefreshVisibility();
    }

    private void LateUpdate()
    {
        RefreshVisibility();
    }

    private void RefreshVisibility()
    {
        SetVisible(burnVfx, burnStatus.IsBurning);
        SetVisible(slowVfx, slowStatus.IsSlowed);
    }

    private static void SetVisible(GameObject vfx, bool visible)
    {
        if (vfx.activeSelf != visible)
        {
            vfx.SetActive(visible);
        }
    }

    private bool ValidateReferences()
    {
        if (burnStatus == null
            || slowStatus == null
            || burnVfx == null
            || slowVfx == null
            || burnVfx == slowVfx
            || burnVfx == gameObject
            || slowVfx == gameObject
            || !burnVfx.transform.IsChildOf(transform)
            || !slowVfx.transform.IsChildOf(transform))
        {
            Debug.LogError(
                "StatusVfxController requires BurnStatus and SlowStatus on the same Enemy Root plus different Burn VFX and Slow VFX child GameObjects.",
                this);
            return false;
        }

        return true;
    }
}
