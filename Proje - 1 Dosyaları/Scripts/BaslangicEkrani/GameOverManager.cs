using UnityEngine;
using UnityEngine.SceneManagement; // Sahne yönetimi için

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverMenu; // Panel UI'yi baðlamak için

    void Start()
    {
        // Menü baþlangýçta gizli olsun
        gameOverMenu.SetActive(false);
    }

    public void PlayerDied()
    {
        // Oyuncu öldüðünde menüyü aç
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f; // Oyunu durdur
    }

    public void RestartGame()
    {
        // Zamaný yeniden baþlat
        Time.timeScale = 1f;



        // Mevcut sahneyi yeniden yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        // Zamaný yeniden baþlat
        Time.timeScale = 1f;

        // Ana menü sahnesine git
        SceneManager.LoadScene("StartScene");
    }
}

