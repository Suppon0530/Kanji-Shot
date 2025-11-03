using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// フェードインクラス
public class FadeinAnimation : MonoBehaviour
{
    [SerializeField] private Image fadeImage;

    private float fadeDuration = 1.5f;


    private void Start()
    {
        StartCoroutine(StartFadeInAnimation());
    }

    IEnumerator StartFadeInAnimation()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.black;

        float startTime = Time.time;
        while (Time.time - startTime < fadeDuration)
        {
            float normalizedTime = (Time.time - startTime) / fadeDuration;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, normalizedTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}