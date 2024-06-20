using System;
using System.Net.NetworkInformation;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownController controller;
    private Vector2 movementDirection = Vector2.zero;
    private CharacterStatHandler characterStatHandler;
    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private void Awake()
    {
        characterStatHandler = GetComponent<CharacterStatHandler>();
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
       controller.OnMoveEvent += Move;
    }
    private void FixedUpdate()
    {
        ApplyMonvement(movementDirection);
    }
    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMonvement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed * Time.deltaTime;

        if (knockbackDuration > 0.0f)
        {
            direction += knockback;
        }
        
        transform.position += new Vector3(direction.x, 0);
    }
}
