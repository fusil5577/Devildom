using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField] private float followRange = 10f;
    [SerializeField] private float shootRange = 5f;
    [SerializeField] private SpriteRenderer characterRenderer;

    private int layerMaskLevel;
    private int layerMaskTarget;

    protected override void Start()
    {
        base.Start();
        layerMaskLevel = LayerMask.NameToLayer("Level");
        layerMaskTarget = stats.CurrentStat.attackSO.target;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // �⺻������ ���� ���¸� false�� �����մϴ�.

        if (distance <= followRange)
        {
            CheckIfNear(distance, direction);
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
            CallMoveEvent(direction); // �����Ÿ� �������� ���� ���� ���� ���� ���, Ÿ�� ������ �̵��մϴ�.
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        // ���� ��ġ���� direction �������� ���̸� �߻��մϴ�.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRaycast());
        Rotate(direction);
        // ���� ������ �ƴ϶� ���� �÷��̾ �¾Ҵ��� Ȯ���մϴ�.
        if (IsTargetHit(hit))
        {
            PerformAttackAction(direction);
        }
        else
        {
           CallMoveEvent(direction);
        }
    }

    private int GetLayerMaskForRaycast()
    {
        // "Level" ���̾�� Ÿ�� ���̾� ��θ� �����ϴ� LayerMask�� ��ȯ�մϴ�.
        return layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D ����� �������� ���� Ÿ���� �����ߴ��� Ȯ���մϴ�.
        return hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // Ÿ���� ��Ȯ�� �������� ����� �ൿ�� �����մϴ�.
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // ���� �߿��� �̵��� ����ϴ�.
        IsAttacking = true;
    }

    private void Rotate(Vector2 direction)
    {
        characterRenderer.flipX = direction.x > 0;
    }
}
