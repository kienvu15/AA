using UnityEngine;

public class FallGround : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool initialActiveState;

    void Start()
    {
        // Lưu vị trí và trạng thái ban đầu
        initialPosition = new Vector2(11.81601f, -13.86186f);
        initialRotation = transform.rotation;
        initialActiveState = gameObject.activeSelf;
    }

    public void GGo()
    {
        if (PlayerMovement.isAlive == false)
        {
            ResetFallGround();
        }
    }

    public void ResetFallGround()
    {
        // Đặt lại vị trí, xoay và trạng thái ban đầu
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        gameObject.SetActive(initialActiveState);
    }
}
