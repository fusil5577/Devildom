using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class MonsterType_C : Monster
{
    GameManager gameManager;
    public MosnterAnimation mosnterAnimation;

    protected bool IsAttacking { get; set; }
    private bool isMoveUp = true;
    private bool isMoveLate = false;

    private float timeSinceLastAttack = float.MaxValue;
    protected Transform ClosestTarget { get; private set; }

    [SerializeField][Range(0f, 100f)] private float followRange;

    [SerializeField] private float shootRange = 5f;

    [SerializeField] private string targetTag = "Player";

    public GameObject fireBall;

    private bool isCollidingWithTarget;

    float AttackTime = 0;
    float AttackDelayTime = 3f;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    [SerializeField] private SpriteRenderer characterRenderer;
    private float autoMovedir = -1;

    private HealthSystem healthSystem;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ClosestTarget = gameManager.Player;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
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
        AttackTime += Time.deltaTime;
        if (AttackTime >= AttackDelayTime)
        {
            RangedAttackSo rangedAttackSo = characterStatHandler.CurrentStat.attackSO as RangedAttackSo;
            CreateProjectileThreedirections(rangedAttackSo);
            AttackTime = 0; 
        }
    }

    private void CreateProjectileThreedirections(RangedAttackSo RangedAttackSO)
    {
        transform.position = projectileSpawnPosition.position;

        for(int i = 0; i < 3;  i++)
        {
            switch(i)
            {
                case 0:
                    GameObject obj = Instantiate(fireBall, transform);
                    ProjectileController attackController = obj.gameObject.GetComponent<ProjectileController>();
                    aimDirection = new Vector2(-1f,1f);
                    attackController.InitializeAttack(aimDirection.normalized, RangedAttackSO);
                    break; 
                case 1:
                    obj = Instantiate(fireBall, transform);
                    attackController = obj.gameObject.GetComponent<ProjectileController>();
                    aimDirection = new Vector2(-1f, 0f);
                    attackController.InitializeAttack(aimDirection.normalized, RangedAttackSO);
                    break; 
                case 2:
                    obj = Instantiate(fireBall, transform);
                    attackController = obj.gameObject.GetComponent<ProjectileController>();
                    aimDirection = new Vector2(-1f, -1f);
                    attackController.InitializeAttack(aimDirection.normalized, RangedAttackSO);
                    break;
            }
            
        }
    }
    private void CreateProjectileEightdirections(RangedAttackSo RangedAttackSO)
    {
        transform.position = projectileSpawnPosition.position;
        GameObject obj = Instantiate(fireBall, transform);
        ProjectileController attackController = obj.gameObject.GetComponent<ProjectileController>();
        attackController.InitializeAttack(aimDirection, RangedAttackSO);
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
}
