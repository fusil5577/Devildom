using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public GameObject SettingPanel;

    private static SceneManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

    public void SettingBtn()
    {
        AudioManager.Instance.PlayButtonClickSound();
        SettingPanel.SetActive(true);
    }

    public void SettingPanelEnd()
    {
        AudioManager.Instance.PlayButtonClickSound();
        SettingPanel.SetActive(false);
    }
}
