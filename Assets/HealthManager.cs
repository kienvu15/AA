using UnityEngine;
using TMPro;

public class HealthManager : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public static HealthManager instance;
    public TextMeshProUGUI livesText;
    public GameObject loseGameUI; // UI hiển thị khi thua

    private void Start()
    {
        currentLives = maxLives;
        UpdateLivesUI();
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void GainLife()
    {
        currentLives++;
        UpdateLivesUI();
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "x" + currentLives;
        }
    }

    private void GameOver()
    {
        loseGameUI.SetActive(true); // Hiển thị UI Lose Game
        Time.timeScale = 0f; // Dừng game
    }

    public int GetLives()
    {
        return currentLives;
    }
}
