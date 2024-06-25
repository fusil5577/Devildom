using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MonsterType_C : Monster
{
    GameManager gameManager;
    public BossMonsterAnimation mosnterAnimation;

    protected bool IsAttacking { get; set; }
    private bool isMoveUp = true;
    private bool isMoveLate = false;

    private float timeSinceLastAttack = float.MaxValue;
    protected Transform ClosestTarget { get; private set; }

    [SerializeField][Range(0f, 100f)] private float followRange;

    [SerializeField] private float shootRange = 5f;

    [SerializeField] private string targetTag = "Player";

    public GameObject fireBall;

    float AttackTime = 0;
    float AttackDelayTime = 3f;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    [SerializeField] private SpriteRenderer characterRenderer;
    private float autoMovedir = -1;

    private HealthSystem healthSystem;
    private HealthSystem collidingTargetHealthSystem;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
        healthSystem.OnDamage += OnDamage;
    }
    private void OnDamage()
    {
        mosnterAnimation.Hit();
    }
    private void OnDeath()
    {
        mosnterAnimation.isAlive = false;
    }
    override protected void FixedUpdate()
    {
        BossMonvement();
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void UpdateEnemyState()
    {
        IsAttacking = false; // 기본적으로 공격 상태를 false로 설정합니다.
    }

    private void HandleAttackDelay()
    {
        if (isMoveLate)
        {
            return;
        }

        AttackTime += Time.deltaTime;
        if (AttackTime >= AttackDelayTime)
        {
            mosnterAnimation.Attack1();
            AttackTime = 0;
            StartCoroutine("BossAttack");
        }
    }

    private void CreateProjectileThreedirections(RangedAttackSo RangedAttackSO)
    {
        Vector2 vector2 = new Vector2(-1f, 1f);
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(fireBall, projectileSpawnPosition);
            ProjectileController attackController = obj.gameObject.GetComponent<ProjectileController>();
            vector2.y = vector2.y - (1 * i);
            aimDirection = vector2;
            attackController.InitializeAttack(aimDirection.normalized, RangedAttackSO);
        }

    }
    private void CreateProjectileEightdirections(RangedAttackSo attackData, int numberOfProjectiles, float spreadHeight)
    {
        float heightStep = spreadHeight / (numberOfProjectiles - 1);
        float startHeight = -spreadHeight / 2;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirYPosition = startHeight + (heightStep * i);
            Vector2 projectileMoveDirection = new Vector2(-1, projectileDirYPosition).normalized;

            GameObject obj = Instantiate(fireBall, projectileSpawnPosition);
            ProjectileController attackController = obj.GetComponent<ProjectileController>();
            attackController.InitializeAttack(projectileMoveDirection, attackData);
        }
    }

    private void BossMonvement()
    {
        if (isMoveLate)
        {
            transform.DOPause();
            return;
        }

        Vector3 pos = transform.position;

        if (isMoveUp == false)
        {
         transform.DOMoveY(pos.y - 10f, 5).SetLoops(2, LoopType.Incremental);
            if (pos.y <= 40f)
            {
                isMoveUp = true;
                isMoveLate = true;
                StartCoroutine("BossLateMove");
            }
        }
        else
        {
         transform.DOMoveY(pos.y + 10f, 5).SetLoops(2, LoopType.Incremental);
            if(pos.y >=60f)
                isMoveUp = false;
        }
    }
    IEnumerator BossLateMove() 
    {
        yield return new WaitForSeconds(3.0f);
        isMoveLate = false;
    }
    IEnumerator BossAttack()
    {
        yield return new WaitForSeconds(0.35f);

        if (healthSystem.CurrentHealth < healthSystem.MaxHealth / 2)
        {
            RangedAttackSo rangedAttackSo = characterStatHandler.CurrentStat.attackSO as RangedAttackSo;
            CreateProjectileEightdirections(rangedAttackSo, 8, 2);
        }
        else
        {
            RangedAttackSo rangedAttackSo = characterStatHandler.CurrentStat.attackSO as RangedAttackSo;
            CreateProjectileThreedirections(rangedAttackSo);
        }
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
            AttackSo attackSO = characterStatHandler.CurrentStat.attackSO;
            bool hasBeenChanged = collidingTargetHealthSystem.ChangeHealth(-attackSO.power);
        }
    }
}
