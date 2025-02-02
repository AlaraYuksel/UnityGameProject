using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro kullanýmý için gerekli

public class LevelManager : MonoBehaviour
{
    public int initialMaxZombies = 5; // Baþlangýçtaki maksimum zombi sayýsý
    public float spawnInterval = 10f; // Zombi spawn aralýðý
    public float levelCooldown = 7f; // Seviye geçiþi için bekleme süresi

    private int maxZombies; // Dinamik olarak deðiþecek maksimum zombi sayýsý
    private int zombiesToSpawn; // Her seviyede spawnlanacak zombi sayýsý
    private int currentZombieCount = 0; // Þu anki toplam zombi sayýsý
    private int currentLevel = 1; // Mevcut seviye numarasý
    private bool levelCompleted = false; // Seviye tamamlanýp tamamlanmadýðýný kontrol eden bayrak

    private int zombieCount;

    // TextMeshPro UI bileþenleri
    public TextMeshProUGUI zombiesToSpawnText; // Spawnlanacak zombi sayýsýný gösteren TextMeshPro
    public TextMeshProUGUI zombiesRemainingText; // Kalan zombi sayýsýný gösteren TextMeshPro
    public TextMeshProUGUI levelText;

    private void Start()
    {
        maxZombies = initialMaxZombies; // Ýlk seviye için maksimum zombi sayýsýný ayarla
        zombiesToSpawn = maxZombies; // Ýlk seviyede spawnlanacak zombi sayýsý
        zombieCount = maxZombies;

        UpdateZombieTexts(); // Ýlk deðerleri UI'ye yansýt
    }

    public bool CanSpawn()
    {
        return currentZombieCount < zombiesToSpawn && !levelCompleted; // Spawn sadece seviye tamamlanmamýþsa yapýlabilir
    }

    public void ZombieSpawned()
    {
        currentZombieCount++;
        UpdateZombieTexts(); // Text güncelle
    }

    public void ZombieDied()
    {
        zombieCount--; // Bir zombi öldü
        UpdateZombieTexts(); // Text güncelle

        if (zombieCount <= 0 && !levelCompleted) // Eðer tüm zombiler öldüyse ve seviye henüz tamamlanmamýþsa
        {
            StartCoroutine(LevelCompleted()); // Seviyeyi tamamlamak için Coroutine'i baþlat
        }
    }

    private IEnumerator LevelCompleted()
    {
        // Seviye tamamlandý, ancak yeni seviyeye geçmeden önce biraz bekleyelim.
        levelCompleted = true; // Seviye tamamlandýðýný iþaretle
        Debug.Log($"Level {currentLevel} tamamlandý! Bir sonraki seviyeye geçiliyor...");
        yield return new WaitForSeconds(levelCooldown); // Seviye geçiþi için cooldown süresi

        // Seviye artýr ve zombi sýnýrýný yükselt
        currentLevel++;
        maxZombies += 2; // Her seviyede zombi sayýsýný artýr
        zombiesToSpawn = maxZombies; // Her seviyede spawnlanacak zombi sayýsý
        zombieCount = maxZombies; // Zombie sayýsýný sýfýrla
        currentZombieCount = 0; // Mevcut zombi sayýsýný sýfýrla
        spawnInterval = spawnInterval * 994 / 1000; // Her seviye artýþýnda spawn süresi kýsaltýlýr.

        levelCompleted = false; // Seviye tamamlandý bayraðýný sýfýrla

        Debug.Log($"Yeni Level: {currentLevel}, Yeni Maks Zombi: {maxZombies}, Yeni Zombi Sayýsý: {zombiesToSpawn}, Yeni Spawn Süresi {spawnInterval}");

        UpdateZombieTexts(); // Text güncelle
    }

    public void ResetLevelData()
    {
        maxZombies = initialMaxZombies; // Ýlk seviyeye geri dön
        zombiesToSpawn = maxZombies;
        currentZombieCount = 0;
        currentLevel = 1;
        levelCompleted = false;
        spawnInterval = 10f; // Baþlangýçtaki spawn aralýðýný tekrar ayarla

        UpdateZombieTexts(); // Text güncelle
    }

    private void UpdateZombieTexts()
    {
        // TextMeshPro UI öðelerini güncelle
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
