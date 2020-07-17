using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _targetToMove;
    NavMeshAgent _navMeshAgent;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        _navMeshAgent.SetDestination(_targetToMove.position);
    }
}
