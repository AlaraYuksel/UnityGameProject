using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Düþmanýn maksimum caný
    private float currentHealth; // Düþmanýn mevcut caný

    [SerializeField]
    private int rewardAmount = 100; // Öldürme ödülü

    private MoneyManager moneyManager; // MoneyManager referansý

    private bool isDead = false; // Öldüðünü kontrol etmek için bayrak

    private void Start()
    {
        currentHealth = maxHealth;

        // MoneyManager'i sahnede bul ve referans al
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager sahnede bulunamadý!");
        }
    }

    

    public void TakeDamage(float damage)
    {
        if (isDead) return; // Eðer zombi zaten öldüyse iþlemi sonlandýr

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true; // Zombiyi ölü olarak iþaretle

            // Kullanýcýya para ekle
            if (moneyManager != null)
            {
                moneyManager.AddMoney(rewardAmount);
            }

            // LevelManager'a düþmanýn öldüðünü bildir
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.ZombieDied();
            }

            Destroy(gameObject); // Can 0 olduðunda düþmaný yok et
        }
    }
}
