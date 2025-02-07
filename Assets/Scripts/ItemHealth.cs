using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f; // Đợi rồi mới hủy để âm thanh kịp phát
    [SerializeField] AudioClip pickupSound; // Âm thanh khi nhặt item
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager healthManager = FindFirstObjectByType<HealthManager>(); // Tìm HealthManager
            if (healthManager != null)
            {
                healthManager.GainLife(); // Cộng 1 mạng

                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = pickupSound;
                audioSource.Play();

                // Ẩn object ngay để tránh va chạm tiếp tục
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;

                // Hủy object sau khi âm thanh phát xong
                Destroy(gameObject, pickupSound.length + destroyDelay);
            }
        }
    }
}
