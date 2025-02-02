using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider veya Image i�in

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen; // Y�klenme ekran� UI'si
    public Slider progressBar; // Y�klenme ilerleme �ubu�u (iste�e ba�l�)
    public TextMeshProUGUI loadingText; // Y�kleniyor yaz�s� (iste�e ba�l�)

    private void Start()
    {
        // Y�klenme ekran�n� gizle, sahne y�klendi�inde aktif edece�iz
        loadingScreen.SetActive(false);
    }

    public void LoadSceneWithLoading(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Y�klenme ekran�n� g�ster
        loadingScreen.SetActive(true);

        // Yava�latma i�in (iste�e ba�l�)
        yield return new WaitForSeconds(0.5f);

        // Sahne y�kleniyor
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Y�klenme bitene kadar bekle
        while (!asyncLoad.isDone)
        {
            // Y�klenme ilerlemesini g�ncelle
            if (progressBar != null)
            {
                progressBar.value = asyncLoad.progress;
            }

            // Y�kleniyor mesaj� (iste�e ba�l�)
            if (loadingText != null)
            {
                loadingText.text = "Y�kleniyor... " + (asyncLoad.progress * 100f).ToString("F0") + "%";
            }

            yield return null;
        }

        // Y�klenme tamamland���nda, ekran� gizle
        loadingScreen.SetActive(false);
    }
}
