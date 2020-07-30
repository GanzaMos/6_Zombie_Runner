using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo9mmPickUpScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponent<Ammo>().IncreaseCurrentAmmo(AmmoType.Bullets9mm, 20);
    }
}
