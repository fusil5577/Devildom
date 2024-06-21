using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    private float FadeOutDuration = 2f;  // 페이드 아웃이 진행되는 시간
    private float FadeInDuration = 2f;  // 페이드 인이 진행되는 시간

    // 페이드 아웃 (흐려짐)
    public void FadeOut(Image fadeOutimage)
    {
        StartCoroutine(Fade(fadeOutimage, 0f, 1f, FadeOutDuration));
    }

    // 페이드 인 (밝아짐)
    public void FadeIn(Image fadeInimage)
    {
        StartCoroutine(Fade(fadeInimage, 1f, 0f, FadeInDuration));
    }

    private IEnumerator Fade(Image image, float startAlpha, float endAlpha, float fadeDuration)
    {
        Color originalColor = image.color;
        float elapsedTime = 0f;

        // 알파 값이 점점 줄어듦
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 최종 알파 값 설정
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
    }
}
