using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour // 임시 게임매니저
{
    public static TestManager Instance;

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public GameObject currentPlayer;
    public ParallaxBackground[] parallaxBackgrounds;
    public CameraFollow cameraFollow;
    public Image Screenimage;
    public FadeImage fadeImage;

    public bool isTalkingToNPC = false;

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        SpawnPlayer();
        fadeImage.FadeIn(Screenimage);
    }

    private void SpawnPlayer()
    {
        fadeImage.FadeIn(Screenimage);

        if (playerPrefab != null && spawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void OnPlayerDeath()
    {
        fadeImage.FadeOut(Screenimage);

        if (currentPlayer != null)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3); // 3초 후에 플레이어를 리스폰
        SpawnPlayer();
    }
}
