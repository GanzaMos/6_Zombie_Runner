using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo12PickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponent<Ammo>().IncreaseCurrentAmmo(AmmoType.Bullets12, 16);
    }
}
