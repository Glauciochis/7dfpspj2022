using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDW : MonoBehaviour
{
    private Firearm firearm;
    public List<GameObject> Rounds;

    void Start() { firearm = GetComponent<Firearm>(); }

    public void FireRound()
    { firearm.Ammo--; UpdateRounds(); if (firearm.Ammo <= 0) { GetComponent<Animator>().SetBool("Firing", false); } }
    public void ReleaseAmmo()
    { firearm.Ammo = 0; UpdateRounds(); }
    public void ReloadAmmo()
    { firearm.Ammo = firearm.MaxAmmo; UpdateRounds(); }

    private void UpdateRounds()
    {
        for (int i = 0; i < Rounds.Count; i++)
        {
            if (firearm.Ammo / 2 <= i) { Rounds[i].SetActive(false); }
            else { Rounds[i].SetActive(true); }
        }
    }
}
