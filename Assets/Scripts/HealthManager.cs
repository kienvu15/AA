using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public static HealthManager instance;
    public TextMeshProUGUI livesText;
    public GameObject loseGameUI; // UI hiển thị khi thua
    GameManager gameManager;

    private void Start()
    {
        ResetLives(); // Đảm bảo mỗi scene khởi đầu với 3 máu
    }

    // Hàm reset lại số mạng khi bắt đầu màn mới
    public void ResetLives()
    {
        currentLives = maxLives; // Đặt lại máu về 3
        UpdateLivesUI();         // Cập nhật UI
        Time.timeScale = 1f;     // Đảm bảo game không bị dừng nếu trước đó thua
    }

    // Hàm mất mạng
    public void LoseLife()
    {
        if (currentLives > 0) // Chỉ trừ máu nếu còn
        {
            currentLives--;
            UpdateLivesUI();
        }

        if (currentLives <= 0) // Nếu hết máu, hiển thị thua game
        {
            GameOver();
        }
    }

    // Hàm tăng máu
    public void GainLife()
    {
        currentLives++;
        UpdateLivesUI();
    }

    // Cập nhật UI số mạng
    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "x" + currentLives;
        }
    }

    // Khi hết mạng, dừng game và hiển thị Lose Panel
    private void GameOver()
    {
        loseGameUI.SetActive(true);
        Time.timeScale = 0f; // Dừng game
    }

    // Trả về số mạng hiện tại
    public int GetLives()
    {
        return currentLives;
    }
}
