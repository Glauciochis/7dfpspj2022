using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemysounds : MonoBehaviour
{
    public AudioSource source;
    private float timer;

    private void Start()
    { timer = Random.Range(5, 240); }
    void Update()
    {
        if (timer <= 0)
        {
            source.Play();
            timer = timer + Random.Range(30, 120);
        }
    }
    void OnAlert() { source.Play(); MusicMixer.Danger++; }
    void OnDeath() { source.Play(); MusicMixer.Danger--; }
}
