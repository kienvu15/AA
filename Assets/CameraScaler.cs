using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public float baseWidth = 4322f;
    public float baseHeight = 1050f;
    public float pixelsPerUnit = 160f;

    private Camera cam;
    private float? overrideCameraSize = null; // Nếu có giá trị này, dùng nó thay vì tính toán lại

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void Update()
    {
        if (overrideCameraSize.HasValue)
        {
            cam.orthographicSize = overrideCameraSize.Value; // Sử dụng giá trị từ `LadderTransition`
        }
        else
        {
            AdjustCamera(); // Nếu không có giá trị override, dùng logic cũ
        }
    }

    void AdjustCamera()
    {
        float targetAspect = baseWidth / baseHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect >= targetAspect)
        {
            cam.orthographicSize = baseHeight / (2f * pixelsPerUnit);
        }
        else
        {
            cam.orthographicSize = baseHeight / (2f * pixelsPerUnit) * (targetAspect / currentAspect);
        }
    }

    // Cập nhật các thông số camera khi chuyển cảnh
    public void UpdateCameraSettings(float newBaseWidth, float newBaseHeight, float newPixelsPerUnit)
    {
        baseWidth = newBaseWidth;
        baseHeight = newBaseHeight;
        pixelsPerUnit = newPixelsPerUnit;
        AdjustCamera();
    }

    // Hàm để đặt giá trị camera tạm thời
    public void SetOverrideCameraSize(float size)
    {
        overrideCameraSize = size;
    }

    // Bỏ giá trị override, quay lại logic tự động
    public void ClearOverrideCameraSize()
    {
        overrideCameraSize = null;
    }
}
