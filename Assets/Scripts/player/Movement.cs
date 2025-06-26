using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PolishedMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    public float jumpPower = 5f;
    public float maxJumpHeight = 2f;
    public float hangTime = 0.15f;
    public float fallBackDuration = 0.5f;

    public bool canDoubleJump = false;

    [Header("Ground Check")]
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Additional Jump Settings")]
    public float coyoteTime = 0.1f;
    public float lowGravityMultiplier = 1.5f;
    public float highGravityMultiplier = 2.5f;

    private bool hasDoubleJumped = false;
    private float velocityY = 0f;
    private float gravity = -9.81f;

    private float hangTimeCounter = 0f;
    private float coyoteTimeCounter = 0f;

    private bool isJumping = false;
    private float initialJumpY;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movement
        float inputX = 0f;
        float inputZ = 0f;

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
            hangTimeCounter = hangTime;
            coyoteTimeCounter = coyoteTime;
            hasDoubleJumped = false;
            isJumping = false;
        }
        else
        {
            hangTimeCounter -= Time.deltaTime;
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Jump Logic
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || hangTimeCounter > 0 || coyoteTimeCounter > 0 || (canDoubleJump && !hasDoubleJumped)))
        {
            velocityY = jumpPower;
            isJumping = true;
            initialJumpY = transform.position.y;

            if (!isGrounded && canDoubleJump)
                hasDoubleJumped = true;

            coyoteTimeCounter = 0f;
        }

        // Variable Jump Logic
        if (Input.GetKey(KeyCode.Space) && isJumping && velocityY > 0f)
        {
            float currentHeight = transform.position.y - initialJumpY;

            if (currentHeight < maxJumpHeight)
            {
                velocityY = Mathf.Lerp(velocityY, jumpPower, Time.deltaTime / fallBackDuration);
            }
            else
            {
                isJumping = false;
            }
        }

        // Variable Gravity
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
