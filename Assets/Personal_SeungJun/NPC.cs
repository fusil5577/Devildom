using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public string[] dialogueLines;
    public bool teleport = false;

    public Transform teleportPosition; // �ڷ���Ʈ ��ġ

    public AudioClip teleportSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Teleport() // �÷��̾� ��ġ ����
    {
        if (teleportPosition != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                audioSource.PlayOneShot(teleportSound);

                player.transform.position = teleportPosition.position;
            }
        }
    }
}