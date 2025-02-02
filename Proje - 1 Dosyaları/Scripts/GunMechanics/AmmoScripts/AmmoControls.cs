using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControls : MonoBehaviour
{
    public WeaponData[] weapons; // Tüm silahlarýn bilgileri

    void Start()
    {

        ResetWeaponAmmo(); // Oyun baþlangýcýnda tüm silahlarý sýfýrla
    }

    private void ResetWeaponAmmo()
    {
        foreach (WeaponData weapon in weapons)
        {
            weapon.totalAmmo = weapon.mustBeTotal; // totalAmmo'yu mustBeTotal ile eþitle
            weapon.currentAmmo = weapon.maxAmmo; // Þarjörü de tam doldur
        }
        Debug.Log("All weapons reset to their default ammo values.");
    }

}
