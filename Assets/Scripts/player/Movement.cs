using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PolishedMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float jumpPower = 5f;
    public float maxJumpHeight = 2f;

    [Header("Timing")]
    public float coyoteTime = 0.15f;

    [Header("Gravity Multipliers")]
    public float lowGravityMultiplier = 1.5f;
    public float highGravityMultiplier = 2.5f;

    [Header("Double Jump")]
    public bool canDoubleJump = false;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool hasDoubleJumped = false;
    private bool isJumping = false;

    private float velocityY = 0f;
    private float gravity = -9.81f;

    private float coyoteTimeCounter = 0f;
    private float initialJumpY;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        float inputX = 0f, inputZ = 0f;
        if (Input.GetKey(KeyCode.W)) inputZ = 1f;
        if (Input.GetKey(KeyCode.S)) inputZ = -1f;
        if (Input.GetKey(KeyCode.A)) inputX = -1f;
        if (Input.GetKey(KeyCode.D)) inputX = 1f;

        Vector3 move = new Vector3(inputX, 0, inputZ);
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Ground Check
        bool isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            velocityY = -1f;
            coyoteTimeCounter = coyoteTime;
            hasDoubleJumped = false;
            isJumping = false;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || coyoteTimeCounter > 0f || (canDoubleJump && !hasDoubleJumped)))
        {
            velocityY = jumpPower;
            isJumping = true;
            initialJumpY = transform.position.y;

            if (!isGrounded && canDoubleJump) hasDoubleJumped = true;

            coyoteTimeCounter = 0f;
        }

        // Variable Jump
        if (Input.GetKey(KeyCode.Space) && isJumping && velocityY > 0f)
        {
            float currentHeight = transform.position.y - initialJumpY;

            if (currentHeight >= maxJumpHeight) isJumping = false;
        }

        // Apply Variable Gravity
        if (velocityY > 0 && !Input.GetKey(KeyCode.Space))
        {
            velocityY += gravity * lowGravityMultiplier * Time.deltaTime;
        }
        else
        {
            velocityY += gravity * highGravityMultiplier * Time.deltaTime;
        }

        // Final Movement
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
