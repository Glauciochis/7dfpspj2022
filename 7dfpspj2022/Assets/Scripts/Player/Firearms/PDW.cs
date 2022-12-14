using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDW : MonoBehaviour
{
    private Firearm firearm;

    public List<GameObject> Rounds;
    public GameObject BulletPrefab;
    public AudioSource FireAudioSource;

    void Start() { firearm = GetComponent<Firearm>(); }

    public void FireRound()
    {
        firearm.Ammo--;
        UpdateRounds();
        if (firearm.Ammo <= 0) { firearm.Reload(); GetComponent<Animator>().SetBool("Firing", false); OnTriggerRelease(); }

        var cam = Camera.main.transform;

        var go = Instantiate(BulletPrefab);
        go.transform.position = cam.position + (cam.forward * .5f);

        Bullet b = go.GetComponent<Bullet>();
        b.Impact = .2f;
        b.Damage = 5f;
        b.Velocity = (cam.forward + (Random.insideUnitSphere * .05f)) * .6f;
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

    void OnTriggerPressure() { FireAudioSource.Play(); }
    void OnTriggerRelease() { FireAudioSource.Stop(); }
}
