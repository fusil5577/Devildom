using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    
    public void LoadMainScene()
    {
        AudioManager.Instance.PlayButtonClickSound();
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    // ��Ʈ�� ������ �̵�
    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    // ���� ������
    public void GameExit()
    {
        AudioManager.Instance.PlayButtonClickSound();
        Application.Quit();
    }

}
