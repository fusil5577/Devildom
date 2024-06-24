using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform Player { get; private set; }

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public GameObject currentPlayer;
    public ParallaxBackground[] parallaxBackgrounds;
    public CameraFollow cameraFollow;
    public Image Screenimage;
    public FadeImage fadeImage;
    public bool isTalkingToNPC = false;

    public AudioClip deathSound;

    private AudioSource audioSource;

    public void StartTalkingToNPC()
    {
        isTalkingToNPC = true;
        
    }

    public void StopTalkingToNPC()
    {
        isTalkingToNPC = false;
    }

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;

        SpawnPlayer();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    private void Start()
    {
        
        fadeImage.FadeIn(Screenimage);
    }

    private void SpawnPlayer()
    {
        fadeImage.FadeIn(Screenimage);

        if (playerPrefab != null && spawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            Player = currentPlayer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        fadeImage.FadeOut(Screenimage);

        audioSource.PlayOneShot(deathSound);

        if (currentPlayer != null)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer() // 3초 후 플레이어 리스폰
    {
        yield return new WaitForSeconds(3);
        SpawnPlayer();
    }
}
