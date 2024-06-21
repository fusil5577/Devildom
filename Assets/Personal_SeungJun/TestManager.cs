using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour // �ӽ� ���ӸŴ���
{
    public static TestManager Instance;

    public GameObject playerPrefab;
    public Transform spawnPoint;
    public GameObject currentPlayer;
    public ParallaxBackground[] parallaxBackgrounds;
    public CameraFollow cameraFollow;
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
    }

    private void SpawnPlayer()
    {
        fadeImage.FadeIn();

        if (playerPrefab != null && spawnPoint != null)
        {
            currentPlayer = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void OnPlayerDeath()
    {
        fadeImage.FadeOut();

        if (currentPlayer != null)
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(3); // 3�� �Ŀ� �÷��̾ ������
        SpawnPlayer();
    }
}