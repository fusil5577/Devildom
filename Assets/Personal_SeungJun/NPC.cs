using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public string[] dialogueLines;
    public bool teleport = false;

    public Transform teleportPosition; // �ڷ���Ʈ ��ġ

    public void Teleport() // �÷��̾� ��ġ ����
    {
        if (teleportPosition != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = teleportPosition.position;
            }
        }
    }
}