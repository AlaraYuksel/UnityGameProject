using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro kullan�m� i�in gerekli

public class LevelManager : MonoBehaviour
{
    public int initialMaxZombies = 5; // Ba�lang��taki maksimum zombi say�s�
    public float spawnInterval = 10f; // Zombi spawn aral���
    public float levelCooldown = 7f; // Seviye ge�i�i i�in bekleme s�resi

    private int maxZombies; // Dinamik olarak de�i�ecek maksimum zombi say�s�
    private int zombiesToSpawn; // Her seviyede spawnlanacak zombi say�s�
    private int currentZombieCount = 0; // �u anki toplam zombi say�s�
    private int currentLevel = 1; // Mevcut seviye numaras�
    private bool levelCompleted = false; // Seviye tamamlan�p tamamlanmad���n� kontrol eden bayrak

    private int zombieCount;

    // TextMeshPro UI bile�enleri
    public TextMeshProUGUI zombiesToSpawnText; // Spawnlanacak zombi say�s�n� g�steren TextMeshPro
    public TextMeshProUGUI zombiesRemainingText; // Kalan zombi say�s�n� g�steren TextMeshPro
    public TextMeshProUGUI levelText;

    private void Start()
    {
        maxZombies = initialMaxZombies; // �lk seviye i�in maksimum zombi say�s�n� ayarla
        zombiesToSpawn = maxZombies; // �lk seviyede spawnlanacak zombi say�s�
        zombieCount = maxZombies;

        UpdateZombieTexts(); // �lk de�erleri UI'ye yans�t
    }

    public bool CanSpawn()
    {
        return currentZombieCount < zombiesToSpawn && !levelCompleted; // Spawn sadece seviye tamamlanmam��sa yap�labilir
    }

    public void ZombieSpawned()
    {
        currentZombieCount++;
        UpdateZombieTexts(); // Text g�ncelle
    }

    public void ZombieDied()
    {
        zombieCount--; // Bir zombi �ld�
        UpdateZombieTexts(); // Text g�ncelle

        if (zombieCount <= 0 && !levelCompleted) // E�er t�m zombiler �ld�yse ve seviye hen�z tamamlanmam��sa
        {
            StartCoroutine(LevelCompleted()); // Seviyeyi tamamlamak i�in Coroutine'i ba�lat
        }
    }

    private IEnumerator LevelCompleted()
    {
        // Seviye tamamland�, ancak yeni seviyeye ge�meden �nce biraz bekleyelim.
        levelCompleted = true; // Seviye tamamland���n� i�aretle
        Debug.Log($"Level {currentLevel} tamamland�! Bir sonraki seviyeye ge�iliyor...");
        yield return new WaitForSeconds(levelCooldown); // Seviye ge�i�i i�in cooldown s�resi

        // Seviye art�r ve zombi s�n�r�n� y�kselt
        currentLevel++;
        maxZombies += 2; // Her seviyede zombi say�s�n� art�r
        zombiesToSpawn = maxZombies; // Her seviyede spawnlanacak zombi say�s�
        zombieCount = maxZombies; // Zombie say�s�n� s�f�rla
        currentZombieCount = 0; // Mevcut zombi say�s�n� s�f�rla
        spawnInterval = spawnInterval * 994 / 1000; // Her seviye art���nda spawn s�resi k�salt�l�r.

        levelCompleted = false; // Seviye tamamland� bayra��n� s�f�rla

        Debug.Log($"Yeni Level: {currentLevel}, Yeni Maks Zombi: {maxZombies}, Yeni Zombi Say�s�: {zombiesToSpawn}, Yeni Spawn S�resi {spawnInterval}");

        UpdateZombieTexts(); // Text g�ncelle
    }

    public void ResetLevelData()
    {
        maxZombies = initialMaxZombies; // �lk seviyeye geri d�n
        zombiesToSpawn = maxZombies;
        currentZombieCount = 0;
        currentLevel = 1;
        levelCompleted = false;
        spawnInterval = 10f; // Ba�lang��taki spawn aral���n� tekrar ayarla

        UpdateZombieTexts(); // Text g�ncelle
    }

    private void UpdateZombieTexts()
    {
        // TextMeshPro UI ��elerini g�ncelle
        if (zombiesToSpawnText != null)
        {
            zombiesToSpawnText.text = $"Zombies to Spawn: {zombiesToSpawn - currentZombieCount}";
        }

        if (zombiesRemainingText != null)
        {
            zombiesRemainingText.text = $"Zombies Remaining: {zombieCount}";
        }

        if(levelText != null)
        {
            levelText.text = $"Level: {currentLevel}";
        }
    }
}
