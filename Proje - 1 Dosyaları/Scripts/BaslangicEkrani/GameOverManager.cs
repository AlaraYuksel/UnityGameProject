using UnityEngine;
using UnityEngine.SceneManagement; // Sahne y�netimi i�in

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverMenu; // Panel UI'yi ba�lamak i�in

    void Start()
    {
        // Men� ba�lang��ta gizli olsun
        gameOverMenu.SetActive(false);
    }

    public void PlayerDied()
    {
        // Oyuncu �ld���nde men�y� a�
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // Oyunu durdur
    }

    public void RestartGame()
    {
        // Zaman� yeniden ba�lat
        Time.timeScale = 1f;



        // Mevcut sahneyi yeniden y�kle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        // Zaman� yeniden ba�lat
        Time.timeScale = 1f;

        // Ana men� sahnesine git
        SceneManager.LoadScene("StartScene");
    }
}

