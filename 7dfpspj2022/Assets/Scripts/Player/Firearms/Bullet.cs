using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float life = 10f;
    public Vector3 Velocity;
    public float Damage;
    public float Impact;
    public GameObject ParticleImpact;

    void Update()
    {
        Vector3 oldp = transform.position;
        transform.position = transform.position + Velocity;
        var jj = oldp - transform.position;
        // Physics.SphereCast(oldp, .1f, jj, out RaycastHit hit, jj.magnitude, ~LayerMask.NameToLayer("Player"))
        if (Physics.SphereCast(oldp, .1f, jj, out RaycastHit hit, jj.magnitude, ~LayerMask.GetMask("Player")))//Physics.Linecast(oldp, transform.position, out RaycastHit hit))
        {
            if (hit.transform.gameObject == null) { return; }
            hit.transform.gameObject.SendMessage("OnShot", this, SendMessageOptions.DontRequireReceiver);

            if (hit.rigidbody) { hit.rigidbody.AddForceAtPosition(hit.normal*300*Impact, hit.point); }

            transform.position = hit.point;

            if (hit.transform.gameObject.isStatic)
            {
                var impgo = Instantiate(ParticleImpact);
                impgo.transform.position = hit.point + (-hit.normal * .3f);
                impgo.transform.rotation = Quaternion.LookRotation(-hit.normal, transform.up);
            }

            Destroy(gameObject);
        }

        life -= Time.deltaTime;
        if (life <= 0) { Destroy(gameObject); }
    }
}
