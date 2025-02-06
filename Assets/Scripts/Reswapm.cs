using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Rigidbody2D rb;
    [SerializeField] private float respawnDelay = 1.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position; // Vị trí bắt đầu là điểm hồi sinh đầu tiên
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        respawnPoint = newCheckpoint;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        // Tắt player tạm thời (nếu cần)
        gameObject.SetActive(false);

        // Đợi 1 giây trước khi hồi sinh
        yield return new WaitForSeconds(respawnDelay);

        // Đưa player về checkpoint
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero; // Reset vận tốc tránh lỗi vật lý

        // Bật lại player
        gameObject.SetActive(true);
    }
}
