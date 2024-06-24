using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public string[] dialogueLines;
    public bool teleport = false;

    public Transform teleportPosition; // 텔레포트 위치

    public AudioClip teleportSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void Teleport() // 플레이어 위치 변경
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