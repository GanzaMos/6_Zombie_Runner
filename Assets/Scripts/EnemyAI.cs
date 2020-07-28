using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] float _detectRadius = 10;
    [SerializeField] float turnSpeed = 30;
    [SerializeField] private float _radiusToProvokeNeighbors = 20f;
    [SerializeField] private float _chanсeToProvokeNeighbors = 0.3f;
    
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
    
    void CheckIsProvoke()
    {
        _distanceToTarget = Vector3.Distance(transform.position, _targetToMove.transform.position);

        if (_distanceToTarget < _detectRadius)
        {
            if (IsPlayerVisible())
            {
                Provoking();
            }
        }
    }

    public void Provoking()
    {
        if (_isProvoke == false)
        {
            ProvokingNeighbors(); 
        }
        _isProvoke = true;
    }

    private void ProvokingNeighbors()
    {
        Collider[] objectsInProvokeRadius = Physics.OverlapSphere(transform.position, _radiusToProvokeNeighbors);
        foreach (var maybeEnemy in objectsInProvokeRadius)
        {
            if (maybeEnemy.GetComponent<EnemyAI>())
            {
                //Debug.Log(maybeEnemy.name);
                //Debug.Log(IsEnemyVisible(maybeEnemy.transform));
                if (IsEnemyVisible(maybeEnemy.transform))
                {
                    maybeEnemy.GetComponent<EnemyAI>()._isProvoke = true;
                    Debug.Log("True");
                }
            }
        }
    }
    
    public bool IsEnemyVisible(Transform maybeEnemy) {
        
        Vector3 startingPoint = new Vector3(
            transform.position.x, 
            transform.position.y + 1, 
            transform.position.z);
        
        Vector3 maybeEnemyPosition = new Vector3(
            maybeEnemy.transform.position.x, 
            maybeEnemy.transform.position.y + 1,
            maybeEnemy.transform.position.z);
        
        Vector3 direction =  maybeEnemyPosition - startingPoint;
        RaycastHit hit;
        
        if (Physics.Raycast(startingPoint, direction, out hit))
        {
            Debug.Log(hit.transform.name);
            var enemy = hit.transform.GetComponent<EnemyHealth>();
            if (enemy) return true;     
        }
        return false;
    }
    
    public bool IsPlayerVisible() {
        Vector3 direction = _targetToMove.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            PlayerHealth player = hit.transform.GetComponent<PlayerHealth>();
            if (player != null) return true;     //definitely a player
        }
        return false;    //didn't hit anything or wasn't a player
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
