using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;    // Men� paneli
    public Slider volumeSlider;       // Ses kontrol slider'�
    private AudioSource[] audioSources; // AudioSource bile�enleri

    void Start()
    {
        // Sahnedeki t�m AudioSource bile�enlerini bul
        audioSources = FindObjectsOfType<AudioSource>();

        // Slider'� ba�lang��ta do�ru de�ere ayarla
        volumeSlider.value = GetVolume();
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);  // Slider de�eri de�i�ince fonksiyon �a�r�l�r

        // Ba�lang��ta men� kapal�
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // ESC tu�una bas�ld���nda men�y� a� veya kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                Resume();  // Men�y� kapat ve oyunu devam ettir
            }
            else
            {
                Pause();  // Men�y� a�
            }
        }
    }

    // Men� a��ld���nda oyun duraklar
    void Pause()
    {
        pauseMenuUI.SetActive(true);  // Men�y� a�
        Time.timeScale = 0f;  // Oyunun zaman�n� duraklat
    }

    // Men� kapat�ld���nda oyun devam eder
    void Resume()
    {
        pauseMenuUI.SetActive(false);  // Men�y� kapat
        Time.timeScale = 1f;  // Oyunun zaman�n� devam ettir
    }

    // Slider de�eri de�i�ti�inde ses seviyesini ayarla
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

    // Ana men�ye d�nme
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;  // Oyunun zaman�n� devam ettir
        SceneManager.LoadScene("MainMenu");  // Ana men� sahnesine ge�i�
    }

    // Oyunu yeniden ba�latma
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Oyunun zaman�n� devam ettir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // �u anki sahneyi yeniden y�kle
    }
}
