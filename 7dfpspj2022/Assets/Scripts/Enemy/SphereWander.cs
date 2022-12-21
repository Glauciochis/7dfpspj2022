using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereWander : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent navagent;
    public float targetRadius;

    void Start()
    {
        navagent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, WorldBase.player.transform.position) <= targetRadius)// && Physics.Linecast(transform.position+new Vector3(0, .5f, 0), WorldBase.player.transform.position, out RaycastHit hit))
        {
            SendMessage("OnAlert", SendMessageOptions.DontRequireReceiver);
            GetComponent<BasicAttackingAI>().enabled = true;
            enabled = false;
            return;
        }

        if (navagent.remainingDistance <= navagent.stoppingDistance)
        {
            animator.SetBool("Walking", true);
            navagent.SetDestination(transform.position + (Random.insideUnitSphere * 4));
        }
    }
}
