using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBenelliM4PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        WeaponScript[] weapons = player.GetComponentsInChildren<WeaponScript>(includeInactive:true);
        foreach (WeaponScript weapon in weapons)
        {
            if (weapon.weaponName == WeaponName.BenelliM4)
            {
                weapon._available = true;
                player.GetComponentInChildren<WeaponSwitcher>().currentWeapon = 1;
            }
        }
    }
}
