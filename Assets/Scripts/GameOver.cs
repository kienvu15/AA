using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void PlayAgain()
    {
        // Load lại scene hiện tại để chơi lại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f; // Đảm bảo game tiếp tục chạy
    }

    public void MainMenu()
    {
        // Chuyển về Scene "Menu"
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f; // Reset tốc độ game
    }
}
