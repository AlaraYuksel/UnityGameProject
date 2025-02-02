using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : Interactable
{
    [SerializeField]
    private GameObject Door; // Kap� nesnesi
    [SerializeField]
    private int doorPrice = 800; // Kap�y� a�ma maliyeti
    private bool doorOpen = false; // Kap�n�n a��k olup olmad���n� kontrol eder
    private bool doorPurchased = false; // Kap�n�n sat�n al�n�p al�nmad���n� kontrol eder

    private MoneyManager moneyManager; // MoneyManager referans�

    void Start()
    {
        // MoneyManager'i sahnede bul ve referans al
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager sahnede bulunamad�!");
        }
    }

    protected override void Interact()
    {
        if (doorPurchased)
        {
            Debug.Log("Bu kap� zaten sat�n al�nd� ve a��k.");
            return;
        }

        if (!doorOpen && moneyManager != null)
        {
            if (moneyManager.GetMoney() >= doorPrice)
            {
                // Para d�� ve kap�y� a�
                moneyManager.DeductMoney(doorPrice);
                doorOpen = true;
                doorPurchased = true; // Kap�n�n sat�n al�nd���n� i�aretle
                Door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
                Debug.Log("Kap� a��ld�!");

                // promptMessage'� bo� b�rak
                promptMessage = "";

                // Interactable �zelli�ini devre d��� b�rak
                this.enabled = false;
            }
            else
            {
                Debug.LogWarning("Yeterli paran�z yok!");
            }
        }
    }
}
