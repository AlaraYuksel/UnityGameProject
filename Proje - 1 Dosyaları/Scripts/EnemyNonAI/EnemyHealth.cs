using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // D��man�n maksimum can�
    private float currentHealth; // D��man�n mevcut can�

    [SerializeField]
    private int rewardAmount = 100; // �ld�rme �d�l�

    private MoneyManager moneyManager; // MoneyManager referans�

    private bool isDead = false; // �ld���n� kontrol etmek i�in bayrak

    private void Start()
    {
        currentHealth = maxHealth;

        // MoneyManager'i sahnede bul ve referans al
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager sahnede bulunamad�!");
        }
    }

    

    public void TakeDamage(float damage)
    {
        if (isDead) return; // E�er zombi zaten �ld�yse i�lemi sonland�r

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true; // Zombiyi �l� olarak i�aretle

            // Kullan�c�ya para ekle
            if (moneyManager != null)
            {
                moneyManager.AddMoney(rewardAmount);
            }

            // LevelManager'a d��man�n �ld���n� bildir
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.ZombieDied();
            }

            Destroy(gameObject); // Can 0 oldu�unda d��man� yok et
        }
    }
}
