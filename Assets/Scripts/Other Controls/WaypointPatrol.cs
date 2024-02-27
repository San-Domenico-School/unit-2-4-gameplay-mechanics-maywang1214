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

public class WaypointPatrol : MonoBehaviour
{
    private GameObject[] waypoints;
    private NavMeshAgent navMeshAgent;
    private int waypointIndex;

    private void Start()
    {
        waypoints = GameManager.Instance.waypoints;
        navMeshAgent = GetComponent<NavMeshAgent>();
        waypointIndex = Random.Range(0,waypoints.Length);
    }

    private void Update()
    {
        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        navMeshAgent.SetDestination(waypoints[waypointIndex].transform.position);
        if(!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }
}
