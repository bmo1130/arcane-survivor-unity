using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float moveSpeed = 5f;

    [SerializeField]
    private InputActionReference moveAction;

    private InputAction moveInputAction;

    private void Awake()
    {
        if (moveAction == null || moveAction.action == null)
        {
            Debug.LogError(
                "PlayerMovement requires the Player/Move input action reference.",
                this);
            enabled = false;
            return;
        }

        moveInputAction = moveAction.action;
    }

    private void OnEnable()
    {
        moveInputAction?.Enable();
    }

    private void OnDisable()
    {
        moveInputAction?.Disable();
    }

    private void Update()
    {
        Vector2 input = Vector2.ClampMagnitude(
            moveInputAction.ReadValue<Vector2>(),
            1f);

        Vector3 movement = new Vector3(input.x, input.y, 0f);
        transform.position += movement * (moveSpeed * Time.deltaTime);
    }
}
