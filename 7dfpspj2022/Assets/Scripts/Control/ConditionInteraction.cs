using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionInteraction : MonoBehaviour
{
    private InteractionObject intobj;

    public string Show;
    public string Item;

    void Start()
    {
        intobj = GetComponent<InteractionObject>();
    }
    void Update()
    {
        if (WorldBase.player.gameObject.GetComponent<RudeInventory>().Items.Contains(Item))
        { intobj.Context = Show; }
    }
}
