using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Gán Canvas PauseMenu vào đây
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Nhấn ESC để bật/tắt Pause Menu
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Dừng thời gian
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục thời gian
        isPaused = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Đảm bảo thời gian chạy bình thường khi load scene mới
        SceneManager.LoadScene(0); // Scene 0 là Menu chính
    }
}
