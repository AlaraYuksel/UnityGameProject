using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Camera playerCamera; // Oyuncu kameras�
    public GameObject weapon;   // Silah modeli

    // Silah�n pozisyonu ve rotas�
    public Vector3 weaponOffset = new Vector3(0.1f, -0.2f, 0.5f); // Silah�n kameradaki yerini ayarlay�n
    public Quaternion weaponRotation = Quaternion.Euler(0, 0, 0); // Silah�n rotas�n� ayarlay�n

    void Start()
    {
        // Silah� kameraya ba�la
        weapon.transform.SetParent(playerCamera.transform);

        // Silah�n pozisyonunu ve rotas�n� ayarla
        weapon.transform.localPosition = weaponOffset;
        weapon.transform.localRotation = weaponRotation;
    }
}
