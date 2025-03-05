using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tornadoEffect;
    [SerializeField] float spinDuration = 5f; 
    [SerializeField] float scaleReduction = 0.5f; 
    [SerializeField] string nextSceneName;
    

    private Transform player; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        TextMeshProUGUI scoreUI = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        ScoreManager.instance.SetScoreText(scoreUI);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Done"))
        {
            StartCoroutine(HandlePlayerSpin());

        }
    }

    

    IEnumerator HandlePlayerSpin()
    {
        GameObject tornado = Instantiate(tornadoEffect, player.position, Quaternion.identity);

        Vector3 originalScale = player.localScale;
        Vector3 targetScale = originalScale * scaleReduction;

        float elapsedTime = 0f;

        while (elapsedTime < spinDuration)
        {
            player.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / spinDuration);

            player.Rotate(0, 0, 360 * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.localScale = originalScale;

        Destroy(tornado);

        SceneManager.LoadScene(nextSceneName);
        
    }
}
