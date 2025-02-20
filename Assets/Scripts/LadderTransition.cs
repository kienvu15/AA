using System.Collections;
using UnityEngine;

public class LadderTransition : MonoBehaviour
{
    public Transform player; // Player cần di chuyển
    public Transform cameraTransform; // Camera cần thay đổi vị trí
    public Camera mainCamera; // Camera chính để thay đổi kích thước

    public Vector3 targetPlayerPosition; // Vị trí Player khi qua màn
    public Vector3 targetCameraPosition; // Vị trí Camera khi qua màn
    public float targetCameraSize; // Kích thước Camera khi qua màn

    // Thông số CameraScaler mới khi chuyển cảnh
    public float newBaseWidth = 4322f;
    public float newBaseHeight = 1050f;
    public float newPixelsPerUnit = 160f;

    private bool isTransitioning = false; // Ngăn chặn spam chuyển cảnh

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTransitioning && collision.CompareTag("Player"))
        {
            StartCoroutine(TransitionScene());
        }
    }

    private IEnumerator TransitionScene()
    {
        isTransitioning = true;
        SceneTransition.Instance.FadeToBlack(() =>
        {
            Debug.Log("FadeToBlack hoàn tất, thay đổi vị trí...");

            CameraScaler cameraScaler = mainCamera.GetComponent<CameraScaler>();
            if (cameraScaler != null)
            {
                // Cập nhật thông số CameraScaler
                cameraScaler.UpdateCameraSettings(newBaseWidth, newBaseHeight, newPixelsPerUnit);
                cameraScaler.SetOverrideCameraSize(targetCameraSize); // Đặt kích thước camera tạm thời
            }

            // Di chuyển Player và Camera
            player.position = targetPlayerPosition;
            cameraTransform.position = targetCameraPosition;
        });

        yield return new WaitForSeconds(1f);

        Debug.Log("FadeFromBlack đang chạy...");
        SceneTransition.Instance.FadeFromBlack();

        // Sau khi hoàn tất chuyển cảnh, cho phép `CameraScaler` hoạt động lại bình thường
        CameraScaler cameraScalerAfter = mainCamera.GetComponent<CameraScaler>();
        if (cameraScalerAfter != null)
        {
            cameraScalerAfter.ClearOverrideCameraSize();
        }

        isTransitioning = false;
    }
}
