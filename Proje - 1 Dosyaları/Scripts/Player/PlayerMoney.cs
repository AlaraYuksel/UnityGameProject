using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int money = 1000; // Oyuncunun ba�lang�� paras�
    public TextMeshProUGUI moneyText; // TextMeshPro referans�

    private void Start()
    {
        money = 1000;
        UpdateMoneyUI(); // Ba�lang��taki para de�erini g�ster
    }

    // Para ekleme
    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log($"Para eklendi: {amount}. Toplam para: {money}");
        UpdateMoneyUI();
    }

    // Para ��karma
    public void DeductMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            Debug.Log($"Para ��kar�ld�: {amount}. Kalan para: {money}");
            UpdateMoneyUI();
        }
        else
        {
            Debug.LogWarning("Yeterli paran�z yok!");
        }
    }

    // Paray� al
    public int GetMoney()
    {
        return money;
    }

    // TextMeshPro'yu g�ncelle
    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Para: {money}";
        }
        else
        {
            Debug.LogWarning("Money TextMeshPro bile�eni atanmad�!");
        }
    }

    public void ResetMoneyData()
    {
        money = 1000; // Para s�f�rlan�r
        UpdateMoneyUI(); // UI'yi g�nceller
    }
}

