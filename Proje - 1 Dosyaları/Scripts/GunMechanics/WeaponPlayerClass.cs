using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Camera playerCamera; // Oyuncu kamerasý
    public GameObject weapon;   // Silah modeli

    // Silahýn pozisyonu ve rotasý
    public Vector3 weaponOffset = new Vector3(0.1f, -0.2f, 0.5f); // Silahýn kameradaki yerini ayarlayýn
    public Quaternion weaponRotation = Quaternion.Euler(0, 0, 0); // Silahýn rotasýný ayarlayýn

    void Start()
    {
        // Silahý kameraya baðla
        weapon.transform.SetParent(playerCamera.transform);

        // Silahýn pozisyonunu ve rotasýný ayarla
        weapon.transform.localPosition = weaponOffset;
        weapon.transform.localRotation = weaponRotation;
    }
}
