using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneSystem : MonoBehaviour
{
    public float Health = 60;
    public float Defense = 1;
    public float Resistance = 0;

    void OnShot(Bullet bullet) { OnDamage(bullet.Damage); }
    void OnDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0) { SendMessage("OnDeath", SendMessageOptions.DontRequireReceiver); }
    }
}
