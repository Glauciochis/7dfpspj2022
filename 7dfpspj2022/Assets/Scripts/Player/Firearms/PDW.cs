using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDW : MonoBehaviour
{
    private Firearm firearm;
    public List<GameObject> Rounds;
    public GameObject BulletPrefab;

    void Start() { firearm = GetComponent<Firearm>(); }

    public void FireRound()
    {
        firearm.Ammo--;
        UpdateRounds();
        if (firearm.Ammo <= 0) { GetComponent<Animator>().SetBool("Firing", false); }

        var cam = Camera.main.transform;

        var go = Instantiate(BulletPrefab);
        go.transform.position = cam.position + (cam.forward * .5f);

        Bullet b = go.GetComponent<Bullet>();
        b.Impact = .2f;
        b.Velocity = (cam.forward + (Random.insideUnitSphere * .15f)) * .6f;
    }
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
