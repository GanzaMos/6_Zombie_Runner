using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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
            FindObjectOfType<DeathHandler>().StartDeath();
        }
    }

    public void IncreasingHealth(int increasingValue)
    {
        playerHitpoint += increasingValue;
    }
}
