using UnityEngine;

public class MouseVisibilityToggle : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Oyuncunun kamerasını referans olarak alacağız
    private bool isMenuActive = false; // ESC menüsü açık mı?
    public bool isPlayerDead = false; // Oyuncu öldü mü?

    private void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogWarning("Oyuncunun kamerası atanmadı! Lütfen Inspector'dan bir kamera atayın.");
            return;
        }

        Time.timeScale = 1f;

        // Oyunun başlangıcında imleci gizle ve kilitle
        LockCursor();
    }

    private void Update()
    {
        // ESC tuşuna basıldığında menüyü aç veya kapat (sadece oyuncu ölü değilse)
        if (Input.GetKeyDown(KeyCode.Escape) && !isPlayerDead)
        {
            isMenuActive = !isMenuActive;

            if (isMenuActive)
            {
                ShowCursor(); // Menüde imleci göster
                Time.timeScale = 0f; // Oyunu durdur
            }
            else
            {
                LockCursor(); // Oyuna geri dön
                Time.timeScale = 1f; // Oyunu devam ettir
            }
        }

        // Oyuncu öldüyse, imleci her zaman görünür yap
        if (isPlayerDead)
        {
            ShowCursor();
            
        }
    }

    private void LockCursor()
    {
        Cursor.visible = false; // Gizle
        Cursor.lockState = CursorLockMode.Locked; // Kilitle
    }

    private void ShowCursor()
    {
        Cursor.visible = true; // Göster
        Cursor.lockState = CursorLockMode.None; // Kilidi aç
    }

    // Oyuncunun öldüğünü bu fonksiyonla bildir
    public void PlayerDied()
    {
        isPlayerDead = true;
    }
}
