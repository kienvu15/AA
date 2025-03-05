using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // Phát âm thanh khi vào cảnh mới
    }
}
