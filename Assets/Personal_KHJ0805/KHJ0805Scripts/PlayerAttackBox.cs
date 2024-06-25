using UnityEngine;
using System.Collections;

public class PlayerAttackBox : MonoBehaviour
{
    private PlayerDefaultAttackSO attackData;

    public void Initialize(PlayerDefaultAttackSO attackData)
    {
        this.attackData = attackData;
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                bool isDamaged = healthSystem.ChangeHealth(-attackData.power);
                if (isDamaged)
                {
                    SpriteRenderer enemyRenderer = collision.GetComponent<SpriteRenderer>();
                    if (enemyRenderer != null)
                    {
                        StartCoroutine(FlashRed(enemyRenderer));
                    }
                }
            }
        }
    }

    private IEnumerator FlashRed(SpriteRenderer renderer)
    {
        Color originalColor = renderer.color;
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = originalColor;
    }
}
