using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject[] guns; // Silah prefablarýný tutacak dizi
    public Transform spawnPoint; // Silahýn spawnlanacaðý nokta

    public float destroyDelay = 4f; // Silahýn kaç saniye sonra silineceði

    // Rastgele bir silah spawnla
    public void SpawnRandomGun()
    {
        if (guns.Length == 0)
        {
            Debug.LogWarning("Guns dizisi boþ! Spawn iþlemi yapýlamaz.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint atanmadý! Spawn iþlemi yapýlamaz.");
            return;
        }

        // Diziden rastgele bir eleman seç
        int randomIndex = Random.Range(0, guns.Length);
        GameObject selectedGun = guns[randomIndex];

        // Seçilen silahý spawnla
        GameObject spawnedGun = Instantiate(selectedGun, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"Spawnlanan silah: {selectedGun.name}");

        // 2 saniye sonra sil
        if (spawnedGun != null)
        {
            Destroy(spawnedGun, destroyDelay);
        }

    }
}

