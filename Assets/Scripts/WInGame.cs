using UnityEngine;
using UnityEngine.SceneManagement;

public class WInGame : MonoBehaviour
{
    public void PlayAgain()
    {
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene("LV1"); // Load màn 1
    }

    public void MainMenu()
    {
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene("Menu");
    }
}
