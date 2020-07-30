using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAk74PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        WeaponScript[] weapons = player.GetComponentsInChildren<WeaponScript>();
        foreach (WeaponScript weapon in weapons)
        {
            if (weapon.weaponName == WeaponName.AK74)
            {
                weapon._available = true;
                player.GetComponentInChildren<WeaponSwitcher>().currentWeapon = 2;
            }
        }
    }
}
