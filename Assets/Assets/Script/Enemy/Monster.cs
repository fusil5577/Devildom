using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected CharacterStatHandler characterStatHandler;
    public Rigidbody2D movementRigidbody;
    private void Awake()
    {
        characterStatHandler = GetComponent<CharacterStatHandler>();
        movementRigidbody = GetComponent<Rigidbody2D>();
    }
        void Start()
    {
        
    }
    protected virtual void FixedUpdate()
    {
        ApplyMonvement(movementDirection);
    }

    void Update()
    {
        
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    private void ApplyMonvement(Vector2 direction)
    {
        direction = direction * characterStatHandler.CurrentStat.speed ;

        movementRigidbody.velocity = new Vector2(direction.x, movementRigidbody.velocity.y);
    }
}
