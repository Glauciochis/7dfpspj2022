using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private Firearm firearm;
    public GameObject BulletPrefab;

    void Start() { firearm = GetComponent<Firearm>(); }

    public void FireRound()
    {
        firearm.Ammo--;
        GetComponent<Animator>().SetBool("Firing", false);

        var cam = Camera.main.transform;

        for (int i = 0; i < Random.Range(12, 24); i++)
        {
            var go = Instantiate(BulletPrefab);
            go.transform.position = cam.position + (cam.forward * .5f);

            Bullet b = go.GetComponent<Bullet>();
            b.Impact = 2f;
            b.Damage = 4f;
            b.Velocity = (cam.forward + (Random.insideUnitSphere * .15f)) * .5f;
        }
    }
    public void ReleaseAmmo()
    { firearm.Ammo = 0; }
    public void ReloadAmmo()
    { firearm.Ammo = firearm.MaxAmmo; }
}
