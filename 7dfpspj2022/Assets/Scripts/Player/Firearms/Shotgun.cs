using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private Firearm firearm;

    void Start() { firearm = GetComponent<Firearm>(); }

    public void FireRound()
    { firearm.Ammo--; GetComponent<Animator>().SetBool("Firing", false); }
    public void ReleaseAmmo()
    { firearm.Ammo = 0; }
    public void ReloadAmmo()
    { firearm.Ammo = firearm.MaxAmmo; }
}
