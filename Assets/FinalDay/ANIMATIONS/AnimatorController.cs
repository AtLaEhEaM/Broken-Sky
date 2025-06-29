using UnityEngine;

public class MotionAnimator : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public Collider groundCheckCollider;
    public LayerMask groundLayer;
    public GameObject jumpEffectObject;
    public float moveThreshold = 0.1f;
    public float groundCheckRadius = 0.2f;

    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip jumpLoopClip;
    public AudioClip landingClip;

    public bool isGrounded;
    private bool wasGrounded;
    private bool isWalking;
    private bool isJumping;
    private Vector3 lastPosition;
    private Vector3 velocity;

    void Start()
    {
        lastPosition = player.position;
        wasGrounded = true;
    }

    void Update()
    {
        Vector3 checkPoint = groundCheckCollider.bounds.center + Vector3.down * groundCheckCollider.bounds.extents.y;
        isGrounded = Physics.CheckSphere(checkPoint, groundCheckRadius, groundLayer);

        Vector3 currentPosition = player.position;
        velocity = (currentPosition - lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;

        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
        float speed = horizontalVelocity.magnitude;

        // Landing sound
        if (isGrounded && !wasGrounded)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(landingClip);
            isJumping = false;
        }

        if (isGrounded)
        {
            jumpEffectObject.SetActive(false);

            if (speed > moveThreshold)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);

                if (!isWalking)
                {
                    PlaySound(walkClip, true);
                    isWalking = true;
                }
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Run", false);
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", false);

                if (isWalking)
                {
                    audioSource.Stop();
                    isWalking = false;
                }
            }
        }
        else
        {
            if (!isJumping)
            {
                PlaySound(jumpLoopClip, true);
                isJumping = true;
                isWalking = false;
            }

            jumpEffectObject.SetActive(true);

            if (velocity.y > 0)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", false);
                animator.SetBool("Jump", true);
                animator.SetBool("Fall", false);
            }
            else
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", false);
                animator.SetBool("Jump", false);
                animator.SetBool("Fall", true);
            }
        }

        wasGrounded = isGrounded;
    }

    private void PlaySound(AudioClip clip, bool loop)
    {
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
}
