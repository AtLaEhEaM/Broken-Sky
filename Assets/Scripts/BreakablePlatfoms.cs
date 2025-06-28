using UnityEngine;

public class BreakablePlatforms : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 20f;
    private bool playerOnPlatform = false;
    private float exitTimer = 0f;
    private Vector3 originalPosition;
    private Vector3 targetDownPosition;
    private bool movingDown = false;
    private bool movingUp = false;

    private void Start()
    {
        originalPosition = transform.position;
        targetDownPosition = originalPosition - new Vector3(0, moveDistance, 0);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            exitTimer = 0f;
            movingDown = true;
            movingUp = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            exitTimer = 0f;
        }
    }

    private void Update()
    {
        if (!playerOnPlatform && movingDown)
        {
            exitTimer += Time.deltaTime;
            if (exitTimer >= 0.5f)
            {
                movingDown = false;
                movingUp = true;
                exitTimer = 0f;
            }
        }

        if (movingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDownPosition, moveSpeed * Time.deltaTime);
        }
        else if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            if (transform.position == originalPosition)
            {
                movingUp = false;
            }
        }
    }
}
