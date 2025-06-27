using UnityEngine;

public class ThirdPersonCameraRotation : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 5f;
    public float rotationSmoothTime = 0.1f;

    private float currentYRotation = 0f;
    private float playerCurrentYRotation = 0f;
    private float playerRotationVelocity = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        currentYRotation += mouseX;

        // Camera rotation
        transform.rotation = Quaternion.Euler(30f, currentYRotation, 0f);

        // Smoothly rotate player to match camera Y rotation
        if (player != null)
        {
            playerCurrentYRotation = Mathf.SmoothDampAngle(
                player.eulerAngles.y,
                currentYRotation,
                ref playerRotationVelocity,
                rotationSmoothTime
            );

            player.rotation = Quaternion.Euler(0f, playerCurrentYRotation, 0f);
        }
    }
}
