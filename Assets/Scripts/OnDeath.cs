using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeTeleportTrigger : MonoBehaviour
{
    public Image fadeImage;
    public Transform player;
    public Transform startPos;
    public float fadeDuration = 1f;

    private bool isFading = false;
    private CharacterController controller;

    void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isFading) return;

        if (other.transform == player)
        {
            StartCoroutine(FadeTeleport());
        }
    }

    IEnumerator FadeTeleport()
    {
        isFading = true;

        // Fade in (0 -> 1)
        yield return StartCoroutine(Fade(0f, 1f, fadeDuration));

        // Teleport: disable controller first
        if (controller != null) controller.enabled = false;
        player.position = startPos.position;
        if (controller != null) controller.enabled = true;

        // Fade out (1 -> 0)
        yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

        isFading = false;
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }
}
