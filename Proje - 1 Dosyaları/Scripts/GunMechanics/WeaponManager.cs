using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData[] weapons;
    public Gun gunScript;
    public Transform weaponHolder;
    private MoneyManager moneyManager;

    public bool stopReloading = false;

    private GameObject currentWeaponPrefab;
    private int currentWeaponIndex = 0;


    private int price = 100;

    void Start()
    {
        moneyManager = GameObject.Find("Player").GetComponent<MoneyManager>();

        EquipWeapon(currentWeaponIndex);
    }

    void Update()
    {
        // Reload iþlemi yapýlýrken silah deðiþimini engelle
        if (gunScript.isReloading) return; // Eðer reload yapýlýyorsa, silah deðiþtirme engellenir

        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex >= weapons.Length) return;

        if (currentWeaponPrefab != null)
        {
            Destroy(currentWeaponPrefab);
        }

        // Mevcut reload iþlemini durdur
        if (gunScript != null)
        {
            gunScript.StopReloading(); // Silah deðiþtirilmeden önce reload durdurulmalý
        }

        currentWeaponIndex = weaponIndex;

        if (weapons[weaponIndex].prefab != null)
        {
            currentWeaponPrefab = Instantiate(
                weapons[weaponIndex].prefab,
                weaponHolder.position,
                weaponHolder.rotation,
                weaponHolder
            );
        }

        gunScript.SetWeaponData(weapons[weaponIndex]);
    }



    public void AssignWeaponData(WeaponData weaponData)
    {
        if (currentWeaponIndex < 0 || currentWeaponIndex >= weapons.Length)
        {
            Debug.LogError("Index is out of range!");
            return;
        }

        weapons[currentWeaponIndex] = weaponData;
        weapons[currentWeaponIndex].currentAmmo = weapons[currentWeaponIndex].maxAmmo;
        weapons[currentWeaponIndex].totalAmmo = weaponData.totalAmmo;
        EquipWeapon(currentWeaponIndex);
    }

}
