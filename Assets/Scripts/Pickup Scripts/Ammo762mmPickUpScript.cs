using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo762mmPickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponent<Ammo>().IncreaseCurrentAmmo(AmmoType.Bullets762mm, 30);
    }
}
