using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchNode : MonoBehaviour
{
    public GameObject Default;
    public string Tag;
    void OnDrawGizmos()
    {
        var f = transform.position + transform.forward;
        Gizmos.DrawLine(transform.position, f);
        Gizmos.DrawLine(f, f - (transform.forward * .3f) + (transform.right * .3f));
        Gizmos.DrawLine(f, f - (transform.forward * .3f) - (transform.right * .3f));
    }
}
