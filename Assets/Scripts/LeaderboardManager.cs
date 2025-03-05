using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] public GameObject nameInputPanel;
    [SerializeField] public GameObject leaderboardPanel;
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_InputField nameInput;
    [SerializeField] public TMP_Text leaderboardText;
    [SerializeField] public Button okButton;
    [SerializeField] public Button playAgainButton;
    [SerializeField] public Button mainMenuButton;

    public int finalScore;

    public void Start()
    {
        finalScore = ScoreManager.instance.GetScore(); // Lấy điểm cuối cùng
        scoreText.text = "Your Score: " + finalScore;
        nameInputPanel.SetActive(true);
        leaderboardPanel.SetActive(false);
        okButton.onClick.AddListener(SaveScore);
        
    }

    public void SaveScore()
    {
        Debug.Log("SaveScore() đã được gọi!");
        string playerName = nameInput.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Tên người chơi trống!");
            return;
        }

        Debug.Log($"Lưu điểm: {playerName} - {finalScore}");

        List<(string, int)> leaderboard = LoadLeaderboard();
        leaderboard.Add((playerName, finalScore));
        leaderboard = leaderboard.OrderByDescending(x => x.Item2).Take(3).ToList();
        SaveLeaderboard(leaderboard);

        nameInputPanel.SetActive(false);
        ShowLeaderboard();
    }


    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        nameInputPanel.SetActive(false);
        List<(string, int)> leaderboard = LoadLeaderboard();
        leaderboardText.text = "Top 3 Players:\n";
        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardText.text += $"{i + 1}. {leaderboard[i].Item1}: {leaderboard[i].Item2}\n";
        }
    }

    
    public void SaveLeaderboard(List<(string, int)> leaderboard)
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            PlayerPrefs.SetString($"Leaderboard_Name_{i}", leaderboard[i].Item1);
            PlayerPrefs.SetInt($"Leaderboard_Score_{i}", leaderboard[i].Item2);
        }
        PlayerPrefs.Save();
    }

    public List<(string, int)> LoadLeaderboard()
    {
        List<(string, int)> leaderboard = new List<(string, int)>();
        for (int i = 0; i < 3; i++)
        {
            string name = PlayerPrefs.GetString($"Leaderboard_Name_{i}", "");
            int score = PlayerPrefs.GetInt($"Leaderboard_Score_{i}", 0);
            if (!string.IsNullOrEmpty(name))
            {
                leaderboard.Add((name, score));
            }
        }
        return leaderboard;
    }
}
