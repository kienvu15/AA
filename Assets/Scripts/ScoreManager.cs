using UnityEngine;
using TMPro; // Nếu dùng TextMeshPro
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        TextMeshProUGUI newScoreUI = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        if (newScoreUI != null)
        {
            ScoreManager.instance.SetScoreText(newScoreUI);
        }
    }

    public void SetScoreText(TextMeshProUGUI newScoreText)
    {
        scoreText = newScoreText;
        UpdateScoreUI();
    }


    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + currentScore;
        }
    }
}
