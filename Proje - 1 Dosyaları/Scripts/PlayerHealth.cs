using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer; //Health bar animasyonu i�in.
    public float maxHealth = 100f;
    public float chipSpeed = 2f; //Ne kadar h�zl� s�rede health bar d��ecek.
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI messageHealth;

    private GameOverManager gameOverManager;
    private MouseVisibilityToggle mouseVisibilityToggle;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        messageHealth = GameObject.FindWithTag("PlayerHealthText").GetComponent<TextMeshProUGUI>();
        gameOverManager = FindObjectOfType<GameOverManager>();
        mouseVisibilityToggle = FindObjectOfType<MouseVisibilityToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        //kapsay�c� min ve max aral���na s�k��t�r�lm�� value d�nd�r�r.
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        ////TakeDamage fonksiyonu i�e yar�yor mu belirlemek i�in kullanaca��m�z kod blo�u.
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    TakeDamage(Random.Range(5,10));
        //}

        ////RestoreHealth fonksiyonu i�e yar�yor mu belirlemek i�in kullanaca��m�z kod blo�u.
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    RestoreHealth(Random.Range(5, 10));
        //}

        if (health <= 0)
        {
            if (gameOverManager != null)
            {
                
                mouseVisibilityToggle.PlayerDied();
                gameOverManager.PlayerDied();
            }
            else
            {
                Debug.LogError("GameOverManager atanmam��!");
            }
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth; //Kesir. Sa�l��� "0.x" tipinde bir fill amounta �evirece�im.
        messageHealth.text = $"{health}/100";
        //Lerpi unutmamak i�in link: https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = hFraction;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
