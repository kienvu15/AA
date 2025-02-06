using UnityEngine;
using System.Collections.Generic; // Dùng Queue để quản lý số lượng mũi tên

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    private static Queue<GameObject> arrowQueue = new Queue<GameObject>(); // Hàng đợi chứa các mũi tên

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // Thêm mũi tên vào hàng đợi
        arrowQueue.Enqueue(gameObject);

        // Nếu có hơn 2 mũi tên, xóa mũi tên đầu tiên (cũ nhất)
        if (arrowQueue.Count > 2)
        {
            GameObject oldArrow = arrowQueue.Dequeue();
            if (oldArrow != null)
            {
                Destroy(oldArrow);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Đổi Rigidbody2D thành Static để dính vào mặt đất
            myRigidbody.bodyType = RigidbodyType2D.Static;
            Debug.Log("true");
        }
    }
}
