using UnityEngine;
using System.Collections;

public class EnemySound : MonoBehaviour
{
    public AudioSource audioSource; // Ses kaynaðý
    public float minInterval = 10f; // Minimum zaman aralýðý
    public float maxInterval = 20f; // Maksimum zaman aralýðý

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource bulunamadý! Lütfen bir AudioSource ekleyin veya atayýn.");
                return;
            }
        }

        // Coroutine baþlat
        StartCoroutine(PlaySoundRoutine());
    }

    IEnumerator PlaySoundRoutine()
    {
        while (true)
        {
            // Sesi çal
            audioSource.Play();

            // Rastgele bir süre bekle
            float randomInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
