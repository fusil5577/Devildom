using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideImage : MonoBehaviour
{
    public GameObject interactionIndicator;

    private bool playerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            interactionIndicator.SetActive(true); // �̹��� Ȱ��ȭ
            StartCoroutine(BlinkIndicator()); // ������
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionIndicator.SetActive(false); // �̹��� ��Ȱ��ȭ
        }
    }

    private IEnumerator BlinkIndicator()
    {
        while (playerInRange)
        {
            interactionIndicator.SetActive(true);
            yield return new WaitForSeconds(1f);
            interactionIndicator.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
