using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName;      // Silah adý
    public float damage;           // Hasar
    public float range;            // Menzil
    public float fireRate;         // Ateþ hýzý
    public float reloadTime;       // Yeniden doldurma süresi

    [Header("Ammo Settings")]
    public int maxAmmo;            // Maksimum þarjör kapasitesi
    [HideInInspector] public int currentAmmo; // Þu anki þarjör mermisi (Inspector'da gizli)
    public int totalAmmo;          // Oyuncunun toplam mermi miktarý

    public int mustBeTotal;

    [Header("Weapon Prefab")]
    public GameObject prefab;      // Silah prefab'ý

    [Header("Audio Settings")]
    public AudioClip shootSound;   // Ateþ etme sesi
    public AudioClip reloadSound;  // Yeniden doldurma sesi
}

