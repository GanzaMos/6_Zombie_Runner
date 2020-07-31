using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1911PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponentInChildren<WeaponSwitcher>().WeaponPickUpHandler(0);
        player.GetComponent<Ammo>().IncreaseCurrentAmmo(AmmoType.Bullets9mm, 20);
    }
}
