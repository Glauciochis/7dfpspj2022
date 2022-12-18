using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSwitch : MonoBehaviour
{
    public MeshFilter meshFilter;

    public bool Switch;
    public Mesh OnMesh;
    public Mesh OffMesh;

    public GameObject Connection;

    void Start()
    {
        meshFilter.mesh = Switch ? OnMesh : OffMesh;
        if (Connection.TryGetComponent<Animator>(out Animator an)) { an.SetBool("Switch", Switch); }
    }
    public void OnInteract(PlayerController player)
    {
        Switch = !Switch;
        meshFilter.mesh = Switch ? OnMesh : OffMesh;

        if (Connection.TryGetComponent<Animator>(out Animator an)) { an.SetBool("Switch", Switch); } 
    }
}
