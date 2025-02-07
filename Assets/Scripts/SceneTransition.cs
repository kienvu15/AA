using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    public CanvasGroup blackScreen;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (blackScreen == null)
        {
            blackScreen = GameObject.Find("BlackScreen")?.GetComponent<CanvasGroup>();
            if (blackScreen == null)
            {
                Debug.LogError("BlackScreen không tồn tại trong Scene!");
            }
        }

        blackScreen.gameObject.SetActive(false); // Ẩn lúc đầu
    }

    public void FadeToBlack(System.Action onComplete)
    {
        blackScreen.gameObject.SetActive(true); // Bật trước khi fade
        StartCoroutine(Fade(1, onComplete));
    }

    public void FadeFromBlack()
    {
        StartCoroutine(Fade(0, () => blackScreen.gameObject.SetActive(false))); // Tắt sau khi fade
    }

    private IEnumerator Fade(float targetAlpha, System.Action onComplete)
    {
        float startAlpha = blackScreen.alpha;
        float time = 0;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        blackScreen.alpha = targetAlpha;

        onComplete?.Invoke(); // Gọi hàm sau khi fade xong
    }
}
