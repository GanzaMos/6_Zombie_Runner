using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] float _detectRadius = 10;
    [SerializeField] float turnSpeed = 30;
    
    Transform _targetToMove;
    NavMeshAgent _navMeshAgent;
    Animator _animator;
    public bool _isProvoke = false;
    private float _distanceToTarget;

    
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _targetToMove = FindObjectOfType<Camera>().transform;
    }
    
    void Update()
    {
        
        CheckIsProvoke();
        
        if (_isProvoke == true)
        {
            //FaceToTarget();
            FaceToTargetWithSpeed();
            EngageTarget();
        }
        else
        {
            _animator.SetTrigger("idle");
        }
    }

    private void FaceToTargetWithSpeed()
    {
        Vector3 diractionToFace = (_targetToMove.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(diractionToFace.x, 0, diractionToFace.z));
        transform.rotation = Quaternion.Slerp(transform.rotation.normalized, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void FaceToTarget()
    {
        transform.LookAt(_targetToMove.position);
    }


    void CheckIsProvoke()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _targetToMove.transform.position);

        if (_distanceToTarget < _detectRadius)
        {
            if (isPlayerVisible() == true)
            {
                _isProvoke = true;
            }
        }
    }
    public bool isPlayerVisible() {
        Vector3 direction = _targetToMove.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player != null) return true;     //definitely a player
        }
        return false;    //didn't hit anything or wasn't a player
    }
    public void ReactionWhenHit()
    {
        _isProvoke = true;
    }

    private void EngageTarget()
    {
        if (_distanceToTarget >= _navMeshAgent.stoppingDistance)
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
