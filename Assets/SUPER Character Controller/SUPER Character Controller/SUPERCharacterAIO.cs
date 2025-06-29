using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SimpleCharacterControllers : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float smoothTime = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    public bool canDoubleJump = false;
    private bool hasDoubleumped = false;
    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 targetVelocity = Vector3.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            targetVelocity = transform.TransformDirection(inputDirection) * moveSpeed;
        }
        else
        {
            targetVelocity = Vector3.zero;
        }

        if ((Input.GetButtonDown("Jump") && isGrounded) || (canDoubleJump && !hasDoubleumped && Input.GetButtonDown("Jump")))

        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasDoubleumped = true;
        }

        if (isGrounded) hasDoubleumped = false;

    }

    void FixedUpdate()
    {
        Vector3 velocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        velocity = Vector3.Lerp(velocity, new Vector3(targetVelocity.x, 0f, targetVelocity.z), smoothTime / Time.fixedDeltaTime);
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }
}