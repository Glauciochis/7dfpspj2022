using UnityEngine;

public class MusicMixer : MonoBehaviour
{
    public AudioSource Track1;
    public AudioSource Track2;
    public float Mix = 0.5f; // 0 = all Track1, 1 = all Track2
    public static float Danger = 0;

    private void Update()
    {
        if (Danger > 0) { Mix = Mathf.Lerp(Mix, 0, .1f); }
        else { Mix = Mathf.Lerp(Mix, 1, .1f); }

        // Set the volume of each track based on the mix value
        Track1.volume = 1 - Mix;
        Track2.volume = Mix;
    }
}