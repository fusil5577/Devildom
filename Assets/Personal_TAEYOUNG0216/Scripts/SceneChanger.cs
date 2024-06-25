using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject SettingPanel;
    // �ΰ��� ������ �̵�
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
