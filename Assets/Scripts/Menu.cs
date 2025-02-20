using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LV1"); // Load màn 1
    }

    public void ExitGame()
    {
        Application.Quit(); // Thoát game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng game nếu đang chạy trong Unity Editor
#endif
    }
}
