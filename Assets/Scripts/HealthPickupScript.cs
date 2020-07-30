using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    void PickUpStuff(Collider player)
    {
        player.GetComponent<PlayerHealth>().IncreasingHealth(25);
    }
}
