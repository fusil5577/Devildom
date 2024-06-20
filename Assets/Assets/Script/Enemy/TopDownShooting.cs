using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class TopDownShooting : MonoBehaviour
{
    private TopDownController controller;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector2 aimDirection = Vector2.right;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
        
    }
    private void Start()
    {
        controller.OnAttackEvent += OnShoot;

        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 newAimDirection)
    {
        aimDirection = newAimDirection;
    }

    private void OnShoot(AttackSo attackso)
    {
        RangedAttackSo rangedAttackSo = attackso as RangedAttackSo;
        if (rangedAttackSo == null) return;

        float projectilesAngleSpace = rangedAttackSo.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = rangedAttackSo.numberofProjectilesPerShot;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            //CreateProjectile(rangedAttackSo);
        }
    }

    private void CreateProjectile(RangedAttackSo RangedAttackSO)
    {
        //transform.position = projectileSpawnPosition.position;
        //ProjectileController attackController = GetComponent<ProjectileController>();
        //attackController.InitializeAttack(aimDirection, RangedAttackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // 벡터 회전하기 : 쿼터니언 * 벡터 순
        return Quaternion.Euler(0, 0, degree) * v;
    }

}


