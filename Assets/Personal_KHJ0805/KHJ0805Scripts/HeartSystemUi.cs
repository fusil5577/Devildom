using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystemUi : MonoBehaviour
{
    private HealthSystem healthSystem;

    public GameObject fullHeart;
    public GameObject emptyHeart;
    public Transform healthContainer;

    private List<GameObject> heartObjects = new List<GameObject>();


    void Start()
    {
        healthSystem = GameManager.Instance.currentPlayer.GetComponent<HealthSystem>();
        InitializeHearts();
        healthSystem.OnDamage += UpdateHearts;
    }

    private void InitializeHearts()
    {
        for (int i = 0; i < healthSystem.MaxHealth; i++)
        {
            GameObject newHeart = Instantiate(fullHeart, healthContainer);
            heartObjects.Add(newHeart);
        }
    }

    private void UpdateHearts()
    {
        int currentHealth = Mathf.CeilToInt(healthSystem.CurrentHealth);

        for (int i = 0; i < heartObjects.Count; i++)
        {
            if (i > currentHealth - 1)
            {
                heartObjects[i].gameObject.GetComponent<Image>().sprite = emptyHeart.GetComponent<Image>().sprite;
            }
        }
    }
}