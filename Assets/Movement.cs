using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _targetToMove;
    [SerializeField] float _detectRadius = 10;
    NavMeshAgent _navMeshAgent;
    private bool _isProvoke = false;
    private float _distanceToTarget;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        
        CheckIsProvoke();
        
        if (_isProvoke == true)
        {
            EngageTarget();
        }
            
    }

    void CheckIsProvoke()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _targetToMove.transform.position);

        if (_distanceToTarget < _detectRadius)
        {
            _isProvoke = true;
        }
        else
        {
            _isProvoke = false;
        }
    }

    private void EngageTarget()
    {
        if (_distanceToTarget < _detectRadius && _distanceToTarget >= _navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (_distanceToTarget < _navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }


    }

    private static void AttackTarget()
    {
        Debug.Log("Attack target");
    }

    private void ChaseTarget()
    {
        _navMeshAgent.SetDestination(_targetToMove.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }
}
