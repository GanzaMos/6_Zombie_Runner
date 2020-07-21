using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private int _enemyHealth = 5;

    public void DecreaseHealth(int _powerOfHit)
    {
        BroadcastMessage("ReactionWhenHit");
        _enemyHealth -= _powerOfHit;
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
