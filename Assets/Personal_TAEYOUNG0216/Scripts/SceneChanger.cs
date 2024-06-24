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
        Application.Quit();
    }

    public void SettingBtn()
    {
        SettingPanel.SetActive(true);
    }

    public void SettingPanelEnd()
    {
        SettingPanel.SetActive(false);
    }
}
