using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int attackPower = 40;
    void Start()
    {
        
    }

    public void HitPerAttack()
    {
        Debug.Log("Hit!");
        target.GetComponent<PlayerHealth>().TakingDamage(attackPower);
    }
}
