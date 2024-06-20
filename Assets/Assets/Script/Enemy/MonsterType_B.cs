using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterType_B : Monster
{
    // Start is called before the first frame update
    GameManager gameManager;
   
    protected bool IsAttacking { get; set; }

    private float timeSinceLastAttack = float.MaxValue;
    protected Transform ClosestTarget { get; private set; }

    [SerializeField][Range(0f, 100f)] private float followRange;
    [SerializeField] private float shootRange = 5f;
    [SerializeField] private string targetTag = "Player";
    public GameObject fireBall;
    private bool isCollidingWithTarget;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    [SerializeField] private SpriteRenderer characterRenderer;
    private float autoMovedir = -1;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;

        InvokeRepeating("AutoMove", 0f, 1.0f);
    }
    private void OnDamage()
    {
        followRange = 100f;
    }
    override protected void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);

    }

    private void Update()
    {
        HandleAttackDelay();
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

    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // 기본적으로 공격 상태를 false로 설정합니다.

        if (distance <= followRange)
        {
            CheckIfNear(distance, direction);
        }
        else
        {
            direction.x += autoMovedir;
            movementDirection = direction;
            Rotate(direction);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        if (distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        else
        {
            movementDirection = direction;
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        
        Rotate(direction);
        movementDirection = Vector2.zero; 
        IsAttacking = true;
    }

    private void HandleAttackDelay()
    {
        if (timeSinceLastAttack <= characterStatHandler.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (IsAttacking && timeSinceLastAttack >= characterStatHandler.CurrentStat.attackSO.delay)
        {
            timeSinceLastAttack = 0f;
         
            RangedAttackSo rangedAttackSo = characterStatHandler.CurrentStat.attackSO as RangedAttackSo;
            CreateProjectile(rangedAttackSo);
        }
    }

    private void CreateProjectile(RangedAttackSo RangedAttackSO)
    {
        transform.position = projectileSpawnPosition.position;
        GameObject obj = Instantiate(fireBall,transform);
        ProjectileController attackController = obj.gameObject.GetComponent<ProjectileController>();
        attackController.InitializeAttack(aimDirection, RangedAttackSO);
    }
}

