using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponM249PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponentInChildren<WeaponSwitcher>().WeaponPickUpHandler(3);
    }
}
