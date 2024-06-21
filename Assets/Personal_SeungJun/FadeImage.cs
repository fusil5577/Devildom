using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeImage : MonoBehaviour
{
    public Image image;
    public float FadeOutDuration = 2f;  // ���̵� �ƿ��� ����Ǵ� �ð�
    public float FadeInDuration = 2f;  // ���̵� ���� ����Ǵ� �ð�

    private void Start()
    {
        FadeIn();
    }

    // ���̵� �ƿ� (�����)
    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f, FadeOutDuration));
    }

    // ���̵� �� (�����)
    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f, FadeInDuration));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float fadeDuration)
    {
        Color originalColor = image.color;
        float elapsedTime = 0f;

        // ���� ���� ���� �پ��
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // ���� ���� �� ����
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, endAlpha);
    }
}
