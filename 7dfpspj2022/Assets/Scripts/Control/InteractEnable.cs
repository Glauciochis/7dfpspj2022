using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractEnable : MonoBehaviour
{
    public string Item;
    public GameObject Other;

    // idk it's almost over dude
    void GOSCENE() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("uwin");
    }

    public void OnInteract(PlayerController player)
    {
        if (player.TryGetComponent(out RudeInventory rd))
        {
            if (rd.Items.Contains(Item))
            {
                Other.SetActive(!Other.activeInHierarchy);
                rd.Items.Remove(Item);
                Invoke("GOSCENE", 2);
            }
        }
    }
}
