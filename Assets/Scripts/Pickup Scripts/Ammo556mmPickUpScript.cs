using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo556mmPickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponent<Ammo>().IncreaseCurrentAmmo(AmmoType.Bullets556х45mm, 30);
    }
}
