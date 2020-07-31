using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon1911PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponentInChildren<WeaponSwitcher>().WeaponPickUpHandler(0);
    }
}
