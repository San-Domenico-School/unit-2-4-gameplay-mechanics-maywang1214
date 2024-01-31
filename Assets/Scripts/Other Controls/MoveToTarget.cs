using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*******************************************************
 * Component of the ice sphere, 
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private GameObject target;
    private Rigidbody targetRb;

    private void Start()
    {
        target = GameObject.Find("Player");
        targetRb = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        navMeshAgent.SetDestination(targetRb.transform.position);
    }
}
