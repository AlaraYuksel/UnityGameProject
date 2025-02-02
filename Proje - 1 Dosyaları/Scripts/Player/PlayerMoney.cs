using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int money = 1000; // Oyuncunun baþlangýç parasý
    public TextMeshProUGUI moneyText; // TextMeshPro referansý

    private void Start()
    {
        money = 1000;
        UpdateMoneyUI(); // Baþlangýçtaki para deðerini göster
    }

    // Para ekleme
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log($"Para eklendi: {amount}. Toplam para: {money}");
        UpdateMoneyUI();
    }

    // Para çýkarma
    public void DeductMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            Debug.Log($"Para çýkarýldý: {amount}. Kalan para: {money}");
            UpdateMoneyUI();
        }
        else
        {
            Debug.LogWarning("Yeterli paranýz yok!");
        }
    }

    // Parayý al
    public int GetMoney()
    {
        return money;
    }

    // TextMeshPro'yu güncelle
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Para: {money}";
        }
        else
        {
            Debug.LogWarning("Money TextMeshPro bileþeni atanmadý!");
        }
    }

    public void ResetMoneyData()
    {
        money = 1000; // Para sýfýrlanýr
        UpdateMoneyUI(); // UI'yi günceller
    }
}

