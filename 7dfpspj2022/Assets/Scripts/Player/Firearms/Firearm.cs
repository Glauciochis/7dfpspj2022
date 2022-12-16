using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firearm : MonoBehaviour
{
    private Animator ani;

    public int MaxAmmo = 8;
    public int Ammo = 8;
    public float Damage = 1;

    void Start() { ani = GetComponent<Animator>(); }

    public void PressTrigger()
    { if (Ammo > 0) { ani.SetBool("Firing", true); } else { ani.SetBool("Reload", true); } }
    public void ReleaseTrigger() { ani.SetBool("Firing", false); }
    public void Reload() { ani.SetBool("Reload", true); }
    public void ReloadComplete() { ani.SetBool("Reload", false); }
}
