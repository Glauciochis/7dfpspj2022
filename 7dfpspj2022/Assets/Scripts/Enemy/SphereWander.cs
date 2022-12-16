using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereWander : MonoBehaviour
{
    private NavMeshAgent navagent;

    void Start()
    {
        navagent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (navagent.remainingDistance <= navagent.stoppingDistance)
        {
            navagent.SetDestination(transform.position + (Random.insideUnitSphere * 4));
        }
    }
}
