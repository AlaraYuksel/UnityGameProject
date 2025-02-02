using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; // Oyuncunun kamerasý
    private float xRotation = 0f; // Kamera yukarý-aþaðý dönüþü için

    public float xSensivity = 30f; // X ekseni hassasiyeti
    public float ySensivity = 30f; // Y ekseni hassasiyeti

    // Fare hareketlerini iþlemek için bir metot
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Y ekseni (yukarý ve aþaðý) bakýþý kontrol et
        xRotation -= mouseY * ySensivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Yukarý-aþaðý sýnýrlama

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Kamerayý döndür

        // X ekseni (saða ve sola) dönüþü kontrol et
        transform.Rotate(Vector3.up * mouseX * xSensivity * Time.deltaTime);
    }
}
