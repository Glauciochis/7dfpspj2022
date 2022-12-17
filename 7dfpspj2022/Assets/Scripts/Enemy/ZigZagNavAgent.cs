using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZigZagNavAgent : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Vector3 currentDestination;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentDestination = target.position;
    }

    void Update()
    {
        agent.SetDestination(currentDestination);

        // If the agent is close to the current destination, choose a new random destination
        if (Vector3.Distance(transform.position, currentDestination) < 2f)
        {
            // Choose a new random destination within a certain radius of the target
            currentDestination = target.position + Random.insideUnitSphere * 10f;
            currentDestination.y = transform.position.y;
        }
    }
}