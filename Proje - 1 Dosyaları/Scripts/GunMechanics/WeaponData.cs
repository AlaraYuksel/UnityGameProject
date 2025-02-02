using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Stats")]
    public string weaponName;      // Silah ad�
    public float damage;           // Hasar
    public float range;            // Menzil
    public float fireRate;         // Ate� h�z�
    public float reloadTime;       // Yeniden doldurma s�resi

    [Header("Ammo Settings")]
    public int maxAmmo;            // Maksimum �arj�r kapasitesi
    [HideInInspector] public int currentAmmo; // �u anki �arj�r mermisi (Inspector'da gizli)
    public int totalAmmo;          // Oyuncunun toplam mermi miktar�

    public int mustBeTotal;

    [Header("Weapon Prefab")]
    public GameObject prefab;      // Silah prefab'�

    [Header("Audio Settings")]
    public AudioClip shootSound;   // Ate� etme sesi
    public AudioClip reloadSound;  // Yeniden doldurma sesi
}

