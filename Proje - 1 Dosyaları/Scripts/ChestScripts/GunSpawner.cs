using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject[] guns; // Silah prefablar�n� tutacak dizi
    public Transform spawnPoint; // Silah�n spawnlanaca�� nokta

    public float destroyDelay = 4f; // Silah�n ka� saniye sonra silinece�i

    // Rastgele bir silah spawnla
    public void SpawnRandomGun()
    {
        if (guns.Length == 0)
        {
            Debug.LogWarning("Guns dizisi bo�! Spawn i�lemi yap�lamaz.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint atanmad�! Spawn i�lemi yap�lamaz.");
            return;
        }

        // Diziden rastgele bir eleman se�
        int randomIndex = Random.Range(0, guns.Length);
        GameObject selectedGun = guns[randomIndex];

        // Se�ilen silah� spawnla
        GameObject spawnedGun = Instantiate(selectedGun, spawnPoint.position, spawnPoint.rotation);
        Debug.Log($"Spawnlanan silah: {selectedGun.name}");

        // 2 saniye sonra sil
        if (spawnedGun != null)
        {
            Destroy(spawnedGun, destroyDelay);
        }

    }
}

