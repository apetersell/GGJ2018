using UnityEngine;
using System.Collections;

public class CrossfadeOnButton : MonoBehaviour
{
    public AudioClip[] tracks;

    //public KeyCode space = KeyCode.Space;
    public float fadeTime = 1.0f;

    private int currentTrack = 0;


    //hook this function into whatever script you need to crossfade with
    public void TrackSwitch ()
    {
        currentTrack++;
        if (currentTrack >= tracks.Length)
        {
            currentTrack = 0;
        }
        //AudioManager.Crossfade(tracks[currentTrack], fadeTime);
    }
}
