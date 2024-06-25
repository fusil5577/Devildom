using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MonsterType_A : Monster
{
    GameManager gameManager;
    public MosnterAnimation mosnterAnimation;
    protected bool IsAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;
    protected Transform ClosestTarget { get; private set; }

    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private string targetTag = "Player";
    private bool isCollidingWithTarget;

    [SerializeField] private SpriteRenderer characterRenderer;

    private HealthSystem healthSystem;
    private HealthSystem collidingTargetHealthSystem;
    private float autoMovedir = -1;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDamage += OnDamage;
        healthSystem.OnDeath += OnDeath;
        InvokeRepeating("AutoMove", 0f, 1.0f);
    }
    private void OnDamage()
    {
        followRange = 100f;
    }
    private void OnDeath()
    {
        mosnterAnimation.isAlive = false;
    }
    override  protected void FixedUpdate()
    {
        base.FixedUpdate();

        if (isCollidingWithTarget)
        {
            ApplyHealthChange();
            mosnterAnimation.isAlive = false;
        }

        Vector2 direction = Vector2.zero;
        if (DistanceToTarget() < followRange)
        {
            direction = DirectionToTarget();
            movementDirection = direction;
            Rotate(direction);
            CancelInvoke("AutoMove");
        }
        else
        {
            direction.x += autoMovedir;
            movementDirection = direction;
            Rotate(direction);
        }

    }

    private void Update() 
    {
    }
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.position - transform.position).normalized;
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
        AttackSo attackSO = characterStatHandler.CurrentStat.attackSO;
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
