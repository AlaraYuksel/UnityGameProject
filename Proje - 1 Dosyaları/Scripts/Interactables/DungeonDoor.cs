using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : Interactable
{
    [SerializeField]
    private GameObject Door; // Kapý nesnesi
    [SerializeField]
    private int doorPrice = 800; // Kapýyý açma maliyeti
    private bool doorOpen = false; // Kapýnýn açýk olup olmadýðýný kontrol eder
    private bool doorPurchased = false; // Kapýnýn satýn alýnýp alýnmadýðýný kontrol eder

    private MoneyManager moneyManager; // MoneyManager referansý

    void Start()
    {
        // MoneyManager'i sahnede bul ve referans al
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager sahnede bulunamadý!");
        }
    }

    protected override void Interact()
    {
        if (doorPurchased)
        {
            Debug.Log("Bu kapý zaten satýn alýndý ve açýk.");
            return;
        }

        if (!doorOpen && moneyManager != null)
        {
            if (moneyManager.GetMoney() >= doorPrice)
            {
                // Para düþ ve kapýyý aç
                moneyManager.DeductMoney(doorPrice);
                doorOpen = true;
                doorPurchased = true; // Kapýnýn satýn alýndýðýný iþaretle
                Door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
                Debug.Log("Kapý açýldý!");

                // promptMessage'ý boþ býrak
                promptMessage = "";

                // Interactable özelliðini devre dýþý býrak
                this.enabled = false;
            }
            else
            {
                Debug.LogWarning("Yeterli paranýz yok!");
            }
        }
    }
}
