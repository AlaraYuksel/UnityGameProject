using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public Animator chestAnimator; // Sand���n Animator bile�eni
    public int chestOpenCost = 900; // Sand��� a�ma maliyeti
    public bool IsChestOpen = false; // Sand���n a��k olup olmad���n� kontrol eden de�i�ken
    public float openDuration = 5f; // Sand���n a��k kalaca�� s�re



    private MoneyManager moneyManager; // MoneyManager referans�

    private void Start()
    {
        // MoneyManager bile�enini sahnedeki herhangi bir nesneden bul
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager bulunamad�! Parayla ilgili i�lemler yap�lamaz.");
        }
    }

    // Sand��� a�ma i�lemi
    public void OpenChest()
    {
        if (IsChestOpen)
        {
            Debug.LogWarning("Sand�k zaten a��k!");
            return;
        }

        if (moneyManager != null && moneyManager.GetMoney() >= chestOpenCost)
        {
            moneyManager.DeductMoney(chestOpenCost); // MoneyManager �zerinden para d��
            chestAnimator.SetBool("IsChestOpened", true); // Animasyona "IsChestOpen" bool de�eri atand�
            IsChestOpen = true;
            Debug.Log("Sand�k a��ld�!");
            GunSpawner gunSpawner = FindObjectOfType<GunSpawner>();
            if (gunSpawner != null)
            {
                gunSpawner.SpawnRandomGun();
            }
            Invoke(nameof(CloseChest), openDuration); // Belirli bir s�re sonra sand��� kapat
        }
        else
        {
            Debug.LogWarning("Yeterli paran�z yok!");
        }
    }

    // Sand��� kapatma i�lemi
    private void CloseChest()
    {
        chestAnimator.SetBool("IsChestOpened", false); // Animasyon kapat�l�yor
        IsChestOpen = false;
        Debug.Log("Sand�k kapand�!");
    }
}
