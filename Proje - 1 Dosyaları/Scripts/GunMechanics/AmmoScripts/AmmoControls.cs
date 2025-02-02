using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControls : MonoBehaviour
{
    public WeaponData[] weapons; // T�m silahlar�n bilgileri

    void Start()
    {

        ResetWeaponAmmo(); // Oyun ba�lang�c�nda t�m silahlar� s�f�rla
    }

    private void ResetWeaponAmmo()
    {
        foreach (WeaponData weapon in weapons)
        {
            weapon.totalAmmo = weapon.mustBeTotal; // totalAmmo'yu mustBeTotal ile e�itle
            weapon.currentAmmo = weapon.maxAmmo; // �arj�r� de tam doldur
        }
        Debug.Log("All weapons reset to their default ammo values.");
    }

}
