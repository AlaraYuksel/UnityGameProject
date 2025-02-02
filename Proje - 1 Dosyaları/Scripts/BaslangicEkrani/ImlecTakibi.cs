using UnityEngine;
using UnityEngine.SceneManagement;

public class SmoothLookAtMouse : MonoBehaviour
{
    public float rotationSpeed = 5f; // Dönüþ hýzý
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
        // Eðer sahne giriþ ekraný deðilse müziði durdur
        if (scene.name != "StartScene")
        {
            OnDestroy();
        }
        
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Event'i kaldýr
    }

    public void Basla() 
    {
        // Fare pozisyonunu al
        Vector3 mouseScreenPosition = Input.mousePosition;

        // Fare pozisyonunu dünya koordinatýna çevir
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.transform.position.y - transform.position.y)
        );

        // Hedef yönü hesapla
        Vector3 direction = mouseWorldPosition - transform.position;

        // Yalnýzca yatay dönüþ için Y eksenine sýnýrla
        direction.y = 0f;

        // Eðer yön sýfýr deðilse dönüþ yap
        if (direction.sqrMagnitude > 0.01f)
        {
            // Hedef rotasyonu hesapla
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Mevcut rotasyonu hedef rotasyona doðru yavaþça döndür
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
