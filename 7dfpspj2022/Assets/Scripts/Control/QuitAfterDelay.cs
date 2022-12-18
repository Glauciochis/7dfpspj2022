using UnityEngine;

public class QuitAfterDelay : MonoBehaviour
{
    public float Delay = 10f;

    private void Start()
    {
        Invoke("Quit", Delay);
    }

    private void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}