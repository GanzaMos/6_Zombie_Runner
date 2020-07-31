using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAk74PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponentInChildren<WeaponSwitcher>().WeaponPickUpHandler(2);
    }
}
