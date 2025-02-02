using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eðer sahne giriþ ekraný deðilse müziði durdur
        if (scene.name != "StartScene")
        {
            audioSource.Stop();
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Event'i kaldýr
    }
}
