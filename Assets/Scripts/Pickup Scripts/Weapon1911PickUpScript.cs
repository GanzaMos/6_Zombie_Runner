using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1911PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        WeaponScript[] weapons = player.GetComponentsInChildren<WeaponScript>();
        foreach (WeaponScript weapon in weapons)
        {
            if (weapon.weaponName == WeaponName.M1911)
            {
                weapon._available = true;
                player.GetComponentInChildren<WeaponSwitcher>().currentWeapon = 0;
            }
        }
    }
}
