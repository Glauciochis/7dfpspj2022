using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAttackingAI : MonoBehaviour
{
    private static List<BasicAttackingAI> ALLACTIVE = new List<BasicAttackingAI>();



    private NavMeshAgent agent;
    public Animator animator;
    public Transform target;
    public enum AIState { Idle, Attacking, Retreating }
    public AIState State;

    public float attackRange = 3.0f;
    public float retreatRange = 5.0f;
    public float rangeVarition = 2.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ALLACTIVE.Add(this);

        target = WorldBase.player.transform;
    }

    void Update()
    {
        switch (State)
        {
            case AIState.Idle:

                break;
            case AIState.Attacking:
                animator.SetBool("Walking", false);
                if (Vector3.Distance(transform.position, target.position) <= attackRange) { Attack(); }
                else { agent.SetDestination(target.position); animator.SetBool("Walking", true); }
                break;
            case AIState.Retreating:
                if (agent.remainingDistance <= agent.stoppingDistance)
                { State = AIState.Attacking; }
                break;
        }
    }

    void Attack()
    {
        agent.isStopped = true;
        //var ap = agent.transform.position;
        //var tap = target.position;
        //var v = new Vector3(0, Mathf.Atan2(ap.z - tap.z, ap.x - tap.x), 0);
        //transform.rotation = Quaternion.Euler(v);
        animator.SetTrigger("Attack");
    }
    public void AttackFinished() { Retreat(); }
    public void AttackClimax()
    {
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            target.SendMessage("OnDamage", Random.Range(5, 20));
        }
    }

    void Retreat()
    {
        State = AIState.Retreating;

        // choose a random direction to retreat in
        Vector3 retreatDirection = Random.onUnitSphere * Random.Range(retreatRange - rangeVarition, retreatRange + rangeVarition);
        retreatDirection += transform.position;

        // move towards the retreat position
        if (agent.enabled)
        {
            agent.isStopped = false;
            agent.SetDestination(retreatDirection);
            animator.SetBool("Walking", true);
        }
    }

    void OnDeath()
    {
        animator.SetBool("Dead", true);
        ALLACTIVE.Remove(this);
        //foreach (var aa in ALLACTIVE) { if (aa != this) { aa.Retreat(); } }
        Collider[] cols = GetComponents<Collider>();
        foreach (var c in cols) { c.enabled = false; }
        if (TryGetComponent(out NavMeshAgent agent)) { agent.enabled = false; }
        if (TryGetComponent(out SphereWander sw)) { sw.enabled = false; }
        enabled = false;
    }
}
