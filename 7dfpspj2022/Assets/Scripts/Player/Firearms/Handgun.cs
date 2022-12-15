using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour
{
    private Firearm firearm;

    public TMPro.TMP_Text AmmoCounter;

    void Start() { firearm = GetComponent<Firearm>(); }
    void Update() { AmmoCounter.text = firearm.Ammo.ToString(); }

    public void FireRound()
    { firearm.Ammo--; GetComponent<Animator>().SetBool("Firing", false); }
    public void ReleaseAmmo()
    { firearm.Ammo = 0; }
    public void ReloadAmmo()
    { firearm.Ammo = firearm.MaxAmmo; }
}
