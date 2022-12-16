using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneSystem : MonoBehaviour
{
    public float Health = 100;
    public float Defense = 1;
    public float Resistance = 0;

    void OnShot(Bullet bullet)
    { Health -= bullet.Damage; }
}
