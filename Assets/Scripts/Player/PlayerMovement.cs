using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerMovement : MonoBehaviour
{
    [SerializeField, Min(0f)]
    private float moveSpeed = 7f;

    [SerializeField]
    private Transform movementCamera;

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

        if (movementCamera == null)
        {
            Debug.LogError(
                "PlayerMovement requires a movement Camera Transform.",
                this);
            enabled = false;
        }
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
        if (Time.timeScale <= 0f || movementCamera == null)
        {
            return;
        }

        Vector2 input = Vector2.ClampMagnitude(
            moveInputAction.ReadValue<Vector2>(),
            1f);

        Vector3 cameraForward = movementCamera.forward;
        Vector3 cameraRight = movementCamera.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        if (cameraForward.sqrMagnitude <= 0.0001f
            || cameraRight.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = cameraRight * input.x + cameraForward * input.y;
        movement = Vector3.ClampMagnitude(movement, 1f);
        transform.position += movement * (moveSpeed * Time.deltaTime);
    }
}
