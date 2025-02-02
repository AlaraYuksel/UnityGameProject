using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam; // Oyuncunun kameras�
    private float xRotation = 0f; // Kamera yukar�-a�a�� d�n��� i�in

    public float xSensivity = 30f; // X ekseni hassasiyeti
    public float ySensivity = 30f; // Y ekseni hassasiyeti

    // Fare hareketlerini i�lemek i�in bir metot
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Y ekseni (yukar� ve a�a��) bak��� kontrol et
        xRotation -= mouseY * ySensivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Yukar�-a�a�� s�n�rlama

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // Kameray� d�nd�r

        // X ekseni (sa�a ve sola) d�n��� kontrol et
        transform.Rotate(Vector3.up * mouseX * xSensivity * Time.deltaTime);
    }
}
