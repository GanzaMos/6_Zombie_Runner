using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private float _enemyHealth = 5;

    public void DecreaseHealth(float _powerOfHit)
    {
        _enemyHealth -= _powerOfHit;
        if (_enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
