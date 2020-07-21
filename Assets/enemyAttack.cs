using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] private int attackPower = 40;
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void HitPerAttack()
    {
        Debug.Log("Hit!");
        target.TakingDamage(attackPower);
    }
}
