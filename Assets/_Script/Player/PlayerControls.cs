using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Movement Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float clampX = 3f;
    [SerializeField] private float clampZ = 2f;

    private Rigidbody rigidBody;
    private Vector2 movementValue;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 currentPosition = rigidBody.position;
        Vector3 moveDirection = new Vector3(movementValue.x, 0, movementValue.y);
        Vector3 newPosition = currentPosition + moveDirection * moveSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -clampX, clampX);
        newPosition.z = Mathf.Clamp(newPosition.z, -clampZ, clampZ);

        rigidBody.MovePosition(newPosition);
    }
}
