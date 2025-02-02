using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.02f;   // Silahýn hareket miktarý
    public float smoothAmount = 3f;    // Hareket yumuþatma miktarý
    public Transform playerCamera;     // Kamera referansý

    private Vector3 initialPosition;

    void Start()
    {
        // Silahýn baþlangýç pozisyonu, silahýn yerel pozisyonunu almak
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Fare hareketine göre silahýn yatay ve dikey hareketini al
        float mouseX = Input.GetAxis("Mouse X") * swayAmount;
        float mouseY = Input.GetAxis("Mouse Y") * swayAmount;

        // Silahýn hedef pozisyonunu oluþtur
        Vector3 targetPosition = new Vector3(
            initialPosition.x + mouseX,
            initialPosition.y + mouseY,
            initialPosition.z
        );

        // Silahýn yerel pozisyonunu yumuþatarak hedef pozisyona kaydýr
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothAmount);
    }
}
