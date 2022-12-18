using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadeadplayerdead : MonoBehaviour
{
    void OnDeath()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ulooz");
    }
}
