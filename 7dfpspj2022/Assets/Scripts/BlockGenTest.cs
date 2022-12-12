using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenTest : MonoBehaviour
{
    public Material RoomMaterial;
    public Material CorridorMaterial;
    public Material DoorMaterial;

    public List<Material> GenericMaterials;

    public ShipInfo Info;
    public GameObject Block;
    public GameObject Plane;
    public List<BlockRoom> Rooms = new List<BlockRoom>();
    public List<RoomInfo> RoomInfos = new List<RoomInfo>();
    
    void Start()
    {
        Info = new ShipInfo();
        Info.Population = 6;
        Info.Venture = ShipInfo.ShipVenture.Mining;
        Info.Density = 50;
        
        /*
        List<byte> whats = new List<byte>();
        whats.Add(0); whats.Add(1); whats.Add(2);
        foreach (var what in whats)
        {
            int w = Random.Range(8, 24); int h = Random.Range(8, 24);
            int xx = Random.Range(-20, 20); int yy = Random.Range(-20, 20);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    GameObject go = Instantiate(Plane);
                    go.transform.position = new Vector3(x+xx, -.5f, y+yy);
                    go.GetComponent<MeshRenderer>().material = GenericMaterials[what];
                }
            }
        }
        // */
        //*
        //Rooms.Add(new BlockRoom(new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
        //Rooms.Add(new BlockRoom(new Vector3(2, 0, 0), new Vector3(4, 1, 3)));
        List<Vector2> pos = new List<Vector2>();
        List<Vector2> dir = new List<Vector2>();
        List<int> len = new List<int>();
        pos.Add(new Vector2(0, 0)); dir.Add(new Vector2(1, 0)); len.Add(0);
        float ii = 0;
        Dictionary<string, byte> map = new Dictionary<string, byte>();
        while (true)
        {

            int ind = Random.Range(0, pos.Count - 1);
            var p = pos[ind]; var d = dir[ind];

            if (!map.ContainsKey(p.x + "," + p.y + ",0"))
            {
                GameObject go = Instantiate(Block);
                go.transform.position = new Vector3(p.x, 0, p.y);
                go.GetComponent<MeshRenderer>().material = CorridorMaterial;
                map.Add(p.x + "," + p.y, 0);

                pos[ind] += d;

                if ((len[ind] > 6 && Random.value > .8f) || len[ind] > 10)
                {
                    dir[ind] = new Vector2(d.y, d.x);
                    if (Random.value < .35f) { dir[ind] = -dir[ind]; }
                    if (Random.value < .2f) { pos.Add(new Vector2(p.x, p.y) - dir[ind]); dir.Add(-dir[ind]); }
                    len[ind] = 0;
                }

                ii++;
                len[ind]++;
            }

            ii = ii + .2f;
            if (ii >= 80) { break; }

        }
        // */
        /*
        for (int i = 0; i < 5; i++)
        {
            Rooms.Add(
                new BlockRoom(
                    new Vector3(Random.Range(-10, 10),
                    0,
                    Random.Range(-10, 10)), new Vector3(2, 1, 2)
                )
            );
        }
        // */
        /*
        foreach (var room in Rooms)
        {
            for (int x = 0; x < room.Scale.x; x++)
            {
                for (int y = 0; y < room.Scale.y; y++)
                {
                    for (int z = 0; z < room.Scale.z; z++)
                    {
                        GameObject go = Instantiate(Block);
                        go.transform.position = room.Position + new Vector3(x, y, z);
                        go.GetComponent<MeshRenderer>().material = RoomMaterial;
                    }
                }
            }
        }
        // */
    }

    bool MakeRoom(BlockRoom room, Dictionary<string, byte> map)
    {

        for (int x = 0; x < room.Scale.x; x++)
        {
            for (int y = 0; y < room.Scale.y; y++)
            {
                for (int z = 0; z < room.Scale.z; z++)
                {
                    if (map.ContainsKey(x + "," + y + "," + z))
                    { return false; }
                }
            }
        }
        
        for (int x = 0; x < room.Scale.x; x++)
        {
            for (int y = 0; y < room.Scale.y; y++)
            {
                for (int z = 0; z < room.Scale.z; z++)
                {
                    GameObject go = Instantiate(Block);
                    go.transform.position = room.Position + new Vector3(x, y, z);
                    go.GetComponent<MeshRenderer>().material = RoomMaterial;
                    map.Add(x + "," + y + "," + z, 1);
                }
            }
        }

        return true;
    }

    public class BlockRoom
    {
        public Vector3 Position;
        public Vector3 Scale;

        public BlockRoom(Vector3 position, Vector3 scale)
        { Position = position; Scale = scale; }
    }
}
