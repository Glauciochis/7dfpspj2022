using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGeneration : MonoBehaviour
{
    public MaterialSet matset;

    void Start()
    {
        MakeObject(matset.Floor, new Vector3(-.5f, 0, -.5f));
    }
    void Update()
    {
        
    }

    void MakeObject(Material mat, Vector3 position)
    {
        // make game object
        GameObject go = new GameObject("thing");
        // make renderer
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = matset.Floor;
        // make filter and collider
        MeshFilter meshFilter = go.AddComponent<MeshFilter>();
        MeshCollider meshCollider = go.AddComponent<MeshCollider>();
        Mesh mesh = new Mesh();

        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector3> nors = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        AddQuad(verts, tris, nors, uvs, position, new Vector3(4, 0, 4));
        AddQuad(verts, tris, nors, uvs, position + new Vector3(5, 0, 0), new Vector3(4, 0, 4));
        AddQuad(verts, tris, nors, uvs, position + new Vector3(-5, 0, 0), new Vector3(4, 0, 4));
        //AddQuad(verts, tris, nors, uvs, position + new Vector3(.5f, 0, 0), new Vector3(1, 1, 0));

        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.normals = nors.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateBounds();
        //mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
    void AddQuad(List<Vector3> verts, List<int> tris, List<Vector3> nors, List<Vector2> uvs, Vector3 position, Vector3 size)
    {
        Vector3[] vertices = new Vector3[4]
        {
            new Vector3(position.x-(size.x*.5f), position.y-(size.y*.5f), position.z-(size.z*.5f)),
            new Vector3(position.x+(size.x*.5f), position.y-(size.y*.5f), position.z-(size.z*.5f)),
            new Vector3(position.x-(size.x*.5f), position.y+(size.y*.5f), position.z+(size.z*.5f)),
            new Vector3(position.x+(size.x*.5f), position.y+(size.y*.5f), position.z+(size.z*.5f))
        };
        verts.AddRange(vertices);

        int[] triangles = new int[6]
        {
            0, 2, 1, // lower left triangle
            2, 3, 1 // upper right triangle
        };
        tris.AddRange(triangles);

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        nors.AddRange(normals);

        Vector2[] uv = new Vector2[4]
        {
              new Vector2(0, 0),
              new Vector2(1, 0),
              new Vector2(0, 1),
              new Vector2(1, 1)
        };
        uvs.AddRange(uv);
    }
}

[Serializable]
public class MaterialSet
{
    public Material Floor;
    public Material Wall;
    public Material Ceiling;
}
