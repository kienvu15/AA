using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public GameObject firePrefab; // Prefab lửa
    public Transform spawnPoint;
    public float fireDuration = 3f; // Thời gian lửa tồn tại
    public float waitDuration = 2f; // Thời gian nghỉ trước khi bắn tiếp

    void Start()
    {
        StartCoroutine(FireCycle());
    }

    IEnumerator FireCycle()
    {
        while (true)
        {
            // Tạo lửa
            GameObject fire = Instantiate(firePrefab, spawnPoint.position, Quaternion.identity);

            // Hủy lửa sau fireDuration giây
            Destroy(fire, fireDuration);

            // Chờ cho đến khi lửa biến mất + thời gian nghỉ
            yield return new WaitForSeconds(fireDuration + waitDuration);
        }
    }
}
