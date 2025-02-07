using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound; // Âm thanh khi nhặt item
    [SerializeField] int pointsForPickup = 1; // Điểm cộng khi nhặt item
    [SerializeField] float destroyDelay = 0.5f; // Đợi rồi mới hủy để âm thanh kịp phát

    private bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;

            // Cộng điểm vào ScoreManager
            ScoreManager.instance.AddScore(pointsForPickup);
            

            // Phát âm thanh bằng AudioSource tạm thời
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
