using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour
{
    private Firearm firearm;

    public TMPro.TMP_Text AmmoCounter;
    public GameObject BulletPrefab;

    void Start() { firearm = GetComponent<Firearm>(); }
    void Update() { AmmoCounter.text = firearm.Ammo.ToString(); }

    public void FireRound()
    {
        firearm.Ammo--;
        GetComponent<Animator>().SetBool("Firing", false);

        var cam = Camera.main.transform;

        var go = Instantiate(BulletPrefab);
        go.transform.position = cam.position + (cam.forward * .5f);

        Bullet b = go.GetComponent<Bullet>();
        b.Impact = .5f;
        b.Velocity = (cam.forward + (Random.insideUnitSphere * .05f)) * .6f;
    }
    public void ReleaseAmmo()
    { firearm.Ammo = 0; }
    public void ReloadAmmo()
    { firearm.Ammo = firearm.MaxAmmo; }
}
