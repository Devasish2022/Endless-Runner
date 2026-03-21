using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("Player Movement Setting")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float clampX = 3f;
    [SerializeField] private float clampZ = 2f;

    [Header("Player Jumping Setting")]
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float gravityValue = -9.81f;

    [Header("Player Grounded Status")]
    [SerializeField][Tooltip("Empty Transform placed under player feet")] private Transform groundCheck;
    [SerializeField] private float groundDistanceRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rigidBody;
    private Vector2 movementValue;
    private Vector3 velocity;
    private bool isJumping = false;
    private bool isGrounded = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // Jump Logic
        if(context.performed && isGrounded) 
        {
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = IsGrounded();
        HandleMovement();
        HandleJumping();
    }

    private void HandleMovement()
    {
        Vector3 currentPosition = rigidBody.position;

        Vector3 moveDirection = new Vector3(movementValue.x, 0, movementValue.y);
        Vector3 newPosition = currentPosition + moveDirection * moveSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -clampX, clampX);
        newPosition.z = Mathf.Clamp(newPosition.z, -clampZ, clampZ);

        rigidBody.MovePosition(new Vector3(newPosition.x, rigidBody.position.y, newPosition.z));
    }

    private void HandleJumping()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            isJumping = false;
        }

        velocity.y += gravityValue * Time.fixedDeltaTime;
        rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, velocity.y, rigidBody.linearVelocity.z);
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
