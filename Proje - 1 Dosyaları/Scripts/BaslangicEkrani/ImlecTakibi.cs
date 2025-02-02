using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothLookAtMouse : MonoBehaviour
{
    public float rotationSpeed = 5f; // D�n�� h�z�
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        Basla();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Basla();
        // E�er sahne giri� ekran� de�ilse m�zi�i durdur
        if (scene.name != "StartScene")
        {
            OnDestroy();
        }
        
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Event'i kald�r
    }

    public void Basla() 
    {
        // Fare pozisyonunu al
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Fare pozisyonunu d�nya koordinat�na �evir
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.transform.position.y - transform.position.y)
        );

        // Hedef y�n� hesapla
        Vector3 direction = mouseWorldPosition - transform.position;

        // Yaln�zca yatay d�n�� i�in Y eksenine s�n�rla
        direction.y = 0f;

        // E�er y�n s�f�r de�ilse d�n�� yap
        if (direction.sqrMagnitude > 0.01f)
        {
            // Hedef rotasyonu hesapla
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Mevcut rotasyonu hedef rotasyona do�ru yava��a d�nd�r
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
