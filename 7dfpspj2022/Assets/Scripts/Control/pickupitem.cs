using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    public string Item;

    public void OnInteract(PlayerController player)
    {
        player.GetComponent<RudeInventory>().Items.Add(Item);
        Destroy(gameObject);
    }
}
