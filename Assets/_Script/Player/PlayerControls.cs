using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Movement Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float clampX = 3f;
    [SerializeField] private float clampZ = 2f;

    [Header("Player Grounded Status")]
    [SerializeField][Tooltip("Empty Transform placed under player feet")] private Transform groundCheck;
    [SerializeField] private float groundDistanceRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

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
        float yPos = rigidBody.position.y;
        Vector3 currentPosition = rigidBody.position;

        if (IsGrounded())
        {
            yPos = 0f;
        }

        Vector3 moveDirection = new Vector3(movementValue.x, yPos, movementValue.y);
        Vector3 newPosition = currentPosition + moveDirection * moveSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -clampX, clampX);
        newPosition.z = Mathf.Clamp(newPosition.z, -clampZ, clampZ);

        rigidBody.MovePosition(newPosition);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundDistanceRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if(groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistanceRadius);
        }
    }

}
