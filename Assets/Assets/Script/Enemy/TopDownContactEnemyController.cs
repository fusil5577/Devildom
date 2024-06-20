using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownContactEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool isCollidingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;

    private HealthSystem healthSystem;
    private HealthSystem collidingTargetHealthSystem;
    private float autoMovedir =-1;

    protected override void Start()
    {
        base.Start();

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage;
        InvokeRepeating("AutoMove", 0f,1.0f);
    }

    private void OnDamage()
    {
        followRange = 100f;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(isCollidingWithTarget ) 
        { 
            ApplyHealthChange();
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange)
        {
            direction = DirectionToTarget();
            CallMoveEvent(direction);
            Rotate(direction);
            CancelInvoke("AutoMove");
        }
        else 
        {
            direction.x += autoMovedir;
            CallMoveEvent(direction);
            Rotate(direction);
        }


    }

    private void Rotate(Vector2 direction)
    { 
        characterRenderer.flipX = direction.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        if (collidingTargetHealthSystem != null)
        {
            isCollidingWithTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag))
        {
            return;
        }

        isCollidingWithTarget = false;
    }

    private void ApplyHealthChange()
    {
        AttackSo attackSO = stats.CurrentStat.attackSO;
        bool hasBeenChanged = collidingTargetHealthSystem.ChangeHealth(-attackSO.power);
    }

    private void AutoMove()
    {
        if (autoMovedir > 0) 
        {
            autoMovedir = -1;
        }
        else
        {
            autoMovedir = 1;
        }
        
    }
}
