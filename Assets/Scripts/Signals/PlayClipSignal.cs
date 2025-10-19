using UnityEngine;

public struct PlayClipSignal
{
    public AudioClip Clip;

    public PlayClipSignal(AudioClip clip)
    {
        Clip = clip;
    }
}