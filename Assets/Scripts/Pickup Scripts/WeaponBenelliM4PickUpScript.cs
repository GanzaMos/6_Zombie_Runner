using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBenelliM4PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponentInChildren<WeaponSwitcher>().WeaponPickUpHandler(1);
    }
}
