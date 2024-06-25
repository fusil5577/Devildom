using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject dialoguePanel;
    public GameObject endPanel;
    public TMP_Text dialogueText;
    public TMP_Text npcNameText;
    private string[] dialogueLines;
    private int currentLineIndex;
    private bool isDialogueActive;
    private GameObject currentNPC;

    public AudioClip displayNextSound;

    private AudioSource audioSource;

    private void Awake()
    {
        endPanel.SetActive(false);

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

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(displayNextSound);

            DisplayNextLine();
        }
    }

    public void StartDialogue(string npcName, string[] lines, GameObject npc)
    {
        npcNameText.text = npcName;
        dialogueLines = lines;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        currentNPC = npc;

        DisplayNextLine();
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        // 움직임 활성화 (임시)
        Time.timeScale = 1f;

        NPC npcController = currentNPC.GetComponent<NPC>();
        if (npcController != null && npcController.teleport)
        {
            GameManager.Instance.fadeImage.FadeOut(GameManager.Instance.Screenimage);
            npcController.Teleport();
            GameManager.Instance.fadeImage.FadeIn(GameManager.Instance.Screenimage);
        }
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }
}
