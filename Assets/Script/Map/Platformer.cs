using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Platformer : MonoBehaviour
{
    private TilemapCollider2D TilemapCollider2D;
    public CrossRoadTriggerBox CrossRoadTriggerBox;

    private void Start()
    {
        TilemapCollider2D = GetComponent<TilemapCollider2D>();
    }

    private void Update()
    {
        if (CrossRoadTriggerBox.IsPlayerInside() && CrossRoadTriggerBox != null)
        {
            TilemapCollider2D.usedByEffector = false;
        }
        else
        {
            TilemapCollider2D.usedByEffector = true;
        }
    }
}
