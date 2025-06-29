
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScene : MonoBehaviour
{
    public Image fadeImage;
    public Transform player;
    public Transform startPos;
    public float fadeDuration = 1f;

    private bool isFading = false;

    public void loadScene(int x)
    {
        Debug.Log("interacted");
        StartCoroutine(Fade(0f, 1f, 1f));
        
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
