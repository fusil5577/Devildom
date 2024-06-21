using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TestManager.Instance.OnPlayerDeath();
            Destroy(other.gameObject);
        }
    }
}
