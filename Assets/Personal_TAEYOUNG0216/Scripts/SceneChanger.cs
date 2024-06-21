using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // �ΰ��� ������ �̵�
    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1.0f;
    }

    // ��Ʈ�� ������ �̵�
    public void LoadIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }

    // ���� ������
    public void GameExit()
    {
        Application.Quit();
    }
}
