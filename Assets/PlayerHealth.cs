using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHitpoint = 100;
    void Start()
    {
        
    }

    public void TakingDamage(int damage)
    {
        playerHitpoint -= damage;
        Debug.Log("You has taken " + damage + " damage. " + playerHitpoint + " left");
        if (playerHitpoint <= 0)
        {
            Debug.Log("You DIE!");
        }
    }
}
