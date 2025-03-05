using UnityEngine;

public class WinLevel : MonoBehaviour
{
    [SerializeField] private AudioClip winSound; // Gán âm thanh từ Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Gán AudioSource trước để tránh tạo mới liên tục
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = winSound;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f; // Đảm bảo âm thanh là 2D
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && winSound != null)
        {
            audioSource.Play();
        }
    }
}
