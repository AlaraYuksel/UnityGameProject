using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider veya Image için

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen; // Yüklenme ekraný UI'si
    public Slider progressBar; // Yüklenme ilerleme çubuðu (isteðe baðlý)
    public TextMeshProUGUI loadingText; // Yükleniyor yazýsý (isteðe baðlý)

    private void Start()
    {
        // Yüklenme ekranýný gizle, sahne yüklendiðinde aktif edeceðiz
        loadingScreen.SetActive(false);
    }

    public void LoadSceneWithLoading(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Yüklenme ekranýný göster
        loadingScreen.SetActive(true);

        // Yavaþlatma için (isteðe baðlý)
        yield return new WaitForSeconds(0.5f);

        // Sahne yükleniyor
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Yüklenme bitene kadar bekle
        while (!asyncLoad.isDone)
        {
            // Yüklenme ilerlemesini güncelle
            if (progressBar != null)
            {
                progressBar.value = asyncLoad.progress;
            }

            // Yükleniyor mesajý (isteðe baðlý)
            if (loadingText != null)
            {
                loadingText.text = "Yükleniyor... " + (asyncLoad.progress * 100f).ToString("F0") + "%";
            }

            yield return null;
        }

        // Yüklenme tamamlandýðýnda, ekraný gizle
        loadingScreen.SetActive(false);
    }
}
