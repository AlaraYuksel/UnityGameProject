using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f;   // Silah�n hareket miktar�
    public float smoothAmount = 3f;    // Hareket yumu�atma miktar�
    public Transform playerCamera;     // Kamera referans�

    private Vector3 initialPosition;

    void Start()
    {
        // Silah�n ba�lang�� pozisyonu, silah�n yerel pozisyonunu almak
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Fare hareketine g�re silah�n yatay ve dikey hareketini al
        float mouseX = Input.GetAxis("Mouse X") * swayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * swayAmount;

        // Silah�n hedef pozisyonunu olu�tur
        Vector3 targetPosition = new Vector3(
            initialPosition.x + mouseX,
            initialPosition.y + mouseY,
            initialPosition.z
        );

        // Silah�n yerel pozisyonunu yumu�atarak hedef pozisyona kayd�r
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothAmount);
    }
}
