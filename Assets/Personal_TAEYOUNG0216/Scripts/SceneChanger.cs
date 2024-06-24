using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
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
}
