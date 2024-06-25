using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    private Coroutine currentFadeCoroutine;  // ���� ���� ���� ���̵� Coroutine

    private float FadeOutDuration = 2f;  // ���̵� �ƿ��� ����Ǵ� �ð�
    private float FadeInDuration = 2f;  // ���̵� ���� ����Ǵ� �ð�

    // ���̵� �ƿ� (�����)
    public void FadeOut(Image fadeOutimage)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);  // ������ ���� ���� Coroutine�� ������ ����
        }
        currentFadeCoroutine = StartCoroutine(Fade(fadeOutimage, 0f, 1f, FadeOutDuration));
    }

    // ���̵� �� (�����)
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

        // ���� ���� ���� �پ��
        while (elapsedTime < fadeDuration)
        {
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // ���� ���� �� ����
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
        currentFadeCoroutine = null;  // Coroutine ���� �� null�� ����
    }
}