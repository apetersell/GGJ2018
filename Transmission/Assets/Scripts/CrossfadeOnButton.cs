using UnityEngine;
using System.Collections;

public class CrossfadeOnButton : MonoBehaviour
{
    public AudioClip[] tracks;

    public KeyCode space = KeyCode.Space;
    public float fadeTime = 1.0f;

    private int currentTrack = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(space))
        {
            currentTrack++;
            if (currentTrack >= tracks.Length)
            {
                currentTrack = 0;
            }
            AudioManager.Crossfade(tracks[currentTrack], fadeTime);
        }
    }
}
