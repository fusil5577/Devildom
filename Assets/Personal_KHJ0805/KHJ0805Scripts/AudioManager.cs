using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    private AudioSource sfxSource;

    public GameObject mainSettingBtn;
    public GameObject settingPanel;
    public GameObject mainToStartBtn;
    public GameObject startSettingBtn;

    public AudioClip startSceneBgm;
    public AudioClip mainSceneBgm;
    public AudioClip buttonClickSfx;

    public AudioMixerGroup bgmGroup;
    public AudioMixerGroup sfxGroup;

    public AudioMixer audioMixer;

    public Slider masterAudioSlider;
    public Slider bgmAudioSlider;
    public Slider sfxAudioSlider;

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

        }
    }

    private void Start()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.outputAudioMixerGroup = bgmGroup;
        sfxSource.outputAudioMixerGroup = sfxGroup;

        bgmSource.loop = true;

        bgmSource.clip = startSceneBgm;
        bgmSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "StartScene":
                bgmSource.clip = startSceneBgm;
                break;
            case "MainScene":
                bgmSource.clip = mainSceneBgm;
                mainSettingBtn.SetActive(true);
                mainToStartBtn.SetActive(true);
                startSettingBtn.SetActive(false);
                break;
        }
        bgmSource.Play();
    }

    public void MasterAudioControl()
    {
        float MasterSlide = masterAudioSlider.value;

        if (MasterSlide == -40f) audioMixer.SetFloat("Master", -80);
        else audioMixer.SetFloat("Master", MasterSlide);
    }

    public void BGMAudioControl()
    {
        float BGMSlide = bgmAudioSlider.value;

        if (BGMSlide == -40f) audioMixer.SetFloat("BGM", -80);
        else audioMixer.SetFloat("BGM", BGMSlide);
    }

    public void SFXAudioControl()
    {
        float SFXSlide = sfxAudioSlider.value;

        if (SFXSlide == -40f) audioMixer.SetFloat("SFX", -80);
        else audioMixer.SetFloat("SFX", SFXSlide);
    }

    public void PlayButtonClickSound()
    {
        sfxSource.PlayOneShot(buttonClickSfx);
    }

    public void MainSceneOpenPanel()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0.0f;
        PlayButtonClickSound();
    }

    public void MainSceneClosePanel()
    {
        settingPanel.SetActive(false);
        Time.timeScale = 1.0f;
        PlayButtonClickSound();
    }

    public void MoveStartSceneBtn()
    {
        SceneManager.LoadScene("StartScene");
        mainToStartBtn.SetActive(false);
        settingPanel.SetActive(false);
        startSettingBtn.SetActive(true);
        Time.timeScale = 1.0f;
        PlayButtonClickSound();
    }

    public void SettingBtn()
    {
        settingPanel.SetActive(true);
        PlayButtonClickSound();
    }
}