using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    private Coroutine currentFadeCoroutine;  // 현재 진행 중인 페이드 Coroutine

    private float FadeOutDuration = 2f;  // 페이드 아웃이 진행되는 시간
    private float FadeInDuration = 2f;  // 페이드 인이 진행되는 시간

    // 페이드 아웃 (흐려짐)
    public void FadeOut(Image fadeOutimage)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);  // 이전에 진행 중인 Coroutine이 있으면 멈춤
        }
        currentFadeCoroutine = StartCoroutine(Fade(fadeOutimage, 0f, 1f, FadeOutDuration));
    }

    // 페이드 인 (밝아짐)
    public void FadeIn(Image fadeInimage)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        currentFadeCoroutine = StartCoroutine(Fade(fadeInimage, 1f, 0f, FadeInDuration));
    }

    private IEnumerator Fade(Image image, float startAlpha, float endAlpha, float fadeDuration)
    {
        Color originalColor = image.color;
        float elapsedTime = 0f;

        // 알파 값이 점점 줄어듦
        while (elapsedTime < fadeDuration)
        {
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 최종 알파 값 설정
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
        currentFadeCoroutine = null;  // Coroutine 종료 후 null로 설정
    }
}