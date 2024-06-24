using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 인게임 씬으로 이동
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    // 인트로 씬으로 이동
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    // 게임 나가기
    public void GameExit()
    {
        Application.Quit();
    }
}
