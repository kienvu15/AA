using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager healthManager = FindFirstObjectByType<HealthManager>(); // Tìm HealthManager
            if (healthManager != null)
            {
                healthManager.GainLife(); // Cộng 1 mạng
                Destroy(gameObject); // Xóa item sau khi ăn
            }
        }
    }
}
