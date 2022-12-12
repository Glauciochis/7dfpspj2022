using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    public List<NodeGroup> Groups;
    public List<HatchNode> HatchNodes;

    void Update()
    {
        while (HatchNodes.Count > 0)
        {
            var hatch = HatchNodes[0];
            NodeGroup g = null; GameObject p;
            foreach (NodeGroup group in Groups) { if (hatch.Tag == group.Tag) { g = group; } }

            int i = Random.Range(0, (g == null) ? 0 : g.Placements.Count);

            if (g == null || i == g.Placements.Count) { p = hatch.Default; }
            else { p = g.Placements[i]; }

            GameObject go = Instantiate(p);
            go.transform.SetParent(hatch.transform);
            PlacementData pd = go.GetComponent<PlacementData>();
            HatchNode hn = pd.HatchNodes[Random.Range(0, pd.HatchNodes.Count-1)];
            /*
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.eulerAngles = hn.transform.eulerAngles - hatch.transform.eulerAngles;
            go.transform.localPosition = hatch.transform.localPosition;
            */

            var newrot = hn.transform.eulerAngles - hatch.transform.eulerAngles - new Vector3(0, 180f, 0);
            go.transform.eulerAngles -= newrot;
            var newpos = hn.transform.position - hatch.transform.position;
            go.transform.position -= newpos;
            go.transform.SetParent(null);

            HatchNodes.RemoveAt(0);
        }
    }
}
