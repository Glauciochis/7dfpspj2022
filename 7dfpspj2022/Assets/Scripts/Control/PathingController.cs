using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingController : MonoBehaviour
{
    public static PathingController _main;
    public List<Node> ActiveNodes;

    void Start() { _main = this; }

    public static Node GetNearestNode(Vector3 pos)
    {
        float cdist = Mathf.Infinity;
        Node cnode = null;
        foreach (Node node in _main.ActiveNodes)
        {
            float dist = (pos - cnode.transform.position).magnitude;
            if (dist < cdist)
            {
                cdist = dist;
                cnode = node;
            }
        }

        return cnode;
    }
    public static Node[] Traceback(Dictionary<Node, Node> camefrom, Node start)
    {
        List<Node> path = new List<Node>();
        path.Add(start);
        Node c = camefrom[start];
        while (true)
        {
            if (camefrom.TryGetValue(c, out Node v))
            {
                path.Add(v);
                c = v;
            }
            else
            {
                break;
            }
        }
        return path.ToArray();
    }
    public static Node[] GetPath(Vector3 start, Node destination) { return GetPath(GetNearestNode(start), destination); }
    public static Node[] GetPath(Node start, Node destination)
    {
        List<Node> frontier = new List<Node>();
        frontier.Add(start);
        Dictionary<Node, Node> CameFrom = new Dictionary<Node, Node>();

        while (frontier.Count > 0)
        {
            Node c = frontier[0];

            if (c == destination)
            {
                return Traceback(CameFrom, destination);
            }

            foreach (Node n in c.Connections)
            {
                if (!CameFrom.ContainsKey(n))
                {
                    CameFrom.Add(n, c);
                    frontier.Add(n);
                }
            }

            frontier.RemoveAt(0);
        }

        return null;
    }
}
