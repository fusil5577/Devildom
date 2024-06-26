using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoadTriggerBox : MonoBehaviour
{
    private bool playerInside = false;  // �÷��̾ �ȿ� �ִ���

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    public bool IsPlayerInside()
    {
        return playerInside;
    }
}
