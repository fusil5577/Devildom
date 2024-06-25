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
    public Transform playerSpawnPoint;
    public GameObject currentPlayer;
    public ParallaxBackground[] parallaxBackgrounds;
    public CameraFollow cameraFollow;
    public Image Screenimage;
    public FadeImage fadeImage;
    public bool isTalkingToNPC = false;

    public AudioClip deathSound;

    private AudioSource audioSource;

    public bool IsPlayerAlive = true;

    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

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

    public void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        }
    }

    private void SpawnPlayer()
    {
        fadeImage.FadeIn(Screenimage);

        if (playerPrefab != null && playerSpawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
            Player = currentPlayer.transform;
        }
    }

    public void OnPlayerDeath()
    {
        fadeImage.FadeOut(Screenimage);

        audioSource.PlayOneShot(deathSound);

        IsPlayerAlive = false;

        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer() // 3�� �� �÷��̾� ������
    {
        yield return new WaitForSeconds(3);
        SpawnPlayer();
        IsPlayerAlive = true;
    }

    public void MoveStartSceneBtn()
    {
        AudioManager.Instance.MoveStartSceneBtn();
    }
}
