using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Vector2 movementDirection = Vector2.zero;
    protected CharacterStatHandler characterStatHandler;
    private void Awake()
    {
        characterStatHandler = GetComponent<CharacterStatHandler>();
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
        direction = direction * characterStatHandler.CurrentStat.speed * Time.deltaTime;

        transform.position += new Vector3(direction.x, 0);
    }
}
