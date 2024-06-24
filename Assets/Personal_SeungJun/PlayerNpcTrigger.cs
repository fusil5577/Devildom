using UnityEngine;

public class PlayerNpcTrigger : MonoBehaviour
{
    private GameObject currentNPC;
    private UIManager uiManager;

    void Start()
    {
        uiManager = UIManager.Instance;
    }

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            NPC npcController = currentNPC.GetComponent<NPC>();
            if (npcController != null)
            {
                uiManager.StartDialogue(npcController.npcName, npcController.dialogueLines, currentNPC);
                // 움직임 비활성화 (임시)
                Time.timeScale = 0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            currentNPC = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            if (currentNPC == other.gameObject)
            {
                currentNPC = null;
            }
        }
    }
}
