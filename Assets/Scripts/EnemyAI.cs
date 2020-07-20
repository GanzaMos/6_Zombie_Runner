using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _targetToMove;
    [SerializeField] float _detectRadius = 10;

    [SerializeField] float turnSpeed = 30;
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    public bool _isProvoke = false;
    private float _distanceToTarget;

    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }
    
    void Update()
    {
        
        CheckIsProvoke();

        if (_isProvoke == true)
        {
            EngageTarget();
        }
        else
        {
            _animator.SetTrigger("idle");
        }
    }
    

    void CheckIsProvoke()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _targetToMove.transform.position);

        if (_distanceToTarget < _detectRadius)
        {
            _isProvoke = true;
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

    private void AttackTarget()
    {
        Debug.Log("Attack target");
        _animator.SetBool("attack", true);
    }

    private void ChaseTarget()
    {
        _animator.SetTrigger("move");
        
        _animator.SetBool("attack", false);
        _navMeshAgent.SetDestination(_targetToMove.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectRadius);
    }
}
