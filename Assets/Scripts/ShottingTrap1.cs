using UnityEngine;

public class ShottingTrap1 : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public float arrowSpeed = 6f; // Tốc độ bay của mũi tên

    void Start()
    {
        InvokeRepeating("SpawnArrow", 0f, 1.8f);
    }

    private void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.right * arrowSpeed;
        }
    }
}
