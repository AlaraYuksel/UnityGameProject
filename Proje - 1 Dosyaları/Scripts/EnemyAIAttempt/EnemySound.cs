using UnityEngine;
using System.Collections;

public class EnemySound : MonoBehaviour
{
    public AudioSource audioSource; // Ses kayna��
    public float minInterval = 10f; // Minimum zaman aral���
    public float maxInterval = 20f; // Maksimum zaman aral���

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource bulunamad�! L�tfen bir AudioSource ekleyin veya atay�n.");
                return;
            }
        }

        // Coroutine ba�lat
        StartCoroutine(PlaySoundRoutine());
    }

    IEnumerator PlaySoundRoutine()
    {
        while (true)
        {
            // Sesi �al
            audioSource.Play();

            // Rastgele bir s�re bekle
            float randomInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
