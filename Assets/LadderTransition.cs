using System.Collections;
using UnityEngine;

public class LadderTransition : MonoBehaviour
{
    public Transform player; // Player cần di chuyển
    public Transform cameraTransform; // Camera cần thay đổi vị trí

    public Vector3 targetPlayerPosition; // Vị trí Player khi qua màn
    public Vector3 targetCameraPosition; // Vị trí Camera khi qua màn

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
            // Dịch chuyển Player và Camera
            player.position = targetPlayerPosition;
            cameraTransform.position = targetCameraPosition;
        });

        yield return new WaitForSeconds(1f); // Chờ một chút trước khi fade in

        SceneTransition.Instance.FadeFromBlack();
        isTransitioning = false;
    }
}
