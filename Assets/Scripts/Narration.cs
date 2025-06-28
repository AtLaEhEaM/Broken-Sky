using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public AudioClip narrationClip;
    public AudioSource audioSource;
    public bool checkpoint = false;
    public Vector3 point;
    public Transform checkpointloc;

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && narrationClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(narrationClip);
            if (checkpoint)
            {
                checkpointloc.localPosition = point;
            }
            Destroy(this);
        }
    }
}
