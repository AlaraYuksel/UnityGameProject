using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;    // Menü paneli
    public Slider volumeSlider;       // Ses kontrol slider'ý
    private AudioSource[] audioSources; // AudioSource bileþenleri

    void Start()
    {
        // Sahnedeki tüm AudioSource bileþenlerini bul
        audioSources = FindObjectsOfType<AudioSource>();

        // Slider'ý baþlangýçta doðru deðere ayarla
        volumeSlider.value = GetVolume();
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);  // Slider deðeri deðiþince fonksiyon çaðrýlýr

        // Baþlangýçta menü kapalý
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // ESC tuþuna basýldýðýnda menüyü aç veya kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();  // Menüyü kapat ve oyunu devam ettir
            }
            else
            {
                Pause();  // Menüyü aç
            }
        }
    }

    // Menü açýldýðýnda oyun duraklar
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Menüyü aç
        Time.timeScale = 0f;  // Oyunun zamanýný duraklat
    }

    // Menü kapatýldýðýnda oyun devam eder
    void Resume()
    {
        pauseMenuUI.SetActive(false);  // Menüyü kapat
        Time.timeScale = 1f;  // Oyunun zamanýný devam ettir
    }

    // Slider deðeri deðiþtiðinde ses seviyesini ayarla
    void OnSliderValueChanged(float value)
    {
        SetVolume(value);
    }

    // Ses seviyesini ayarla
    void SetVolume(float volume)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    // Mevcut ses seviyesini al
    float GetVolume()
    {
        return audioSources.Length > 0 ? audioSources[0].volume : 1f;
    }

    // Ana menüye dönme
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;  // Oyunun zamanýný devam ettir
        SceneManager.LoadScene("MainMenu");  // Ana menü sahnesine geçiþ
    }

    // Oyunu yeniden baþlatma
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Oyunun zamanýný devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Þu anki sahneyi yeniden yükle
    }
}
