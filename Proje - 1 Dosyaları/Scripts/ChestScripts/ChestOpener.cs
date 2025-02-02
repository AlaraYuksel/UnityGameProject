using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public Animator chestAnimator; // Sandýðýn Animator bileþeni
    public int chestOpenCost = 900; // Sandýðý açma maliyeti
    public bool IsChestOpen = false; // Sandýðýn açýk olup olmadýðýný kontrol eden deðiþken
    public float openDuration = 5f; // Sandýðýn açýk kalacaðý süre



    private MoneyManager moneyManager; // MoneyManager referansý

    private void Start()
    {
        // MoneyManager bileþenini sahnedeki herhangi bir nesneden bul
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager bulunamadý! Parayla ilgili iþlemler yapýlamaz.");
        }
    }

    // Sandýðý açma iþlemi
    public void OpenChest()
    {
        if (IsChestOpen)
        {
            Debug.LogWarning("Sandýk zaten açýk!");
            return;
        }

        if (moneyManager != null && moneyManager.GetMoney() >= chestOpenCost)
        {
            moneyManager.DeductMoney(chestOpenCost); // MoneyManager üzerinden para düþ
            chestAnimator.SetBool("IsChestOpened", true); // Animasyona "IsChestOpen" bool deðeri atandý
            IsChestOpen = true;
            Debug.Log("Sandýk açýldý!");
            GunSpawner gunSpawner = FindObjectOfType<GunSpawner>();
            if (gunSpawner != null)
            {
                gunSpawner.SpawnRandomGun();
            }
            Invoke(nameof(CloseChest), openDuration); // Belirli bir süre sonra sandýðý kapat
        }
        else
        {
            Debug.LogWarning("Yeterli paranýz yok!");
        }
    }

    // Sandýðý kapatma iþlemi
    private void CloseChest()
    {
        chestAnimator.SetBool("IsChestOpened", false); // Animasyon kapatýlýyor
        IsChestOpen = false;
        Debug.Log("Sandýk kapandý!");
    }
}
