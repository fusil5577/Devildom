using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void LoadMainScene()
    {
        AudioManager.Instance.PlayButtonClickSound();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
    }

    public void GameExit()
    {
        AudioManager.Instance.PlayButtonClickSound();
        Application.Quit();
    }
}
