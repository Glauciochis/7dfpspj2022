using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] Connections;
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, .25f);
        foreach (Node conn in Connections)
        {
            Gizmos.DrawLine(transform.position, conn.transform.position);
        }
    }
}
