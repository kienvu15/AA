using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerMovement pl;

    private void Awake()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra Player chạm vào checkpoint
        {
            pl.UpDateCheckpoint(transform.position);
        }
    }
}
