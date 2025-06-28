using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public AudioClip narrationClip;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && narrationClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(narrationClip);
        }
    }
}
