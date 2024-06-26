using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterType_B : Monster
{
    // Start is called before the first frame update
    GameManager gameManager;
    public MosnterAnimation mosnterAnimation;

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

    public AudioClip fireballSound;

    private AudioSource fireballAudioSource;

    private HealthSystem healthSystem;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;

        fireballAudioSource = gameObject.AddComponent<AudioSource>();
        fireballAudioSource.clip = fireballSound;
        fireballAudioSource.playOnAwake = false;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
    }
    private void OnDeath()
    {
        mosnterAnimation.isAlive = false;
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
        if (ClosestTarget == null)
        {
            return float.MaxValue;
        }
        return Vector3.Distance(transform.position, ClosestTarget.position);
    }

    protected Vector2 DirectionToTarget()
    {
        if (ClosestTarget == null)
        {
            return Vector2.zero;
        }
        return (ClosestTarget.position - transform.position).normalized;
    }

    private void Rotate(Vector2 direction)
    {
        characterRenderer.flipX = direction.x > 0;
        aimDirection = direction;
    }

    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // 기본적으로 공격 상태를 false로 설정합니다.

        if (distance <= followRange)
        {
            CheckIfNear(distance, direction);
            Rotate(direction);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        if (distance <= shootRange)
        {
            TryShootAtTarget(direction);
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

        fireballAudioSource.Play();
    }
}

