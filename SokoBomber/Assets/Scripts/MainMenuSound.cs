using UnityEngine;
using System.Collections;

public class MainMenuSound : MonoBehaviour
{

    public AudioClip[] heavyRockSounds;
    public AudioClip[] lightRockSounds;
    public AudioClip gemDrop;
    // Use this for initialization

    public void PlayRandomLightRockSound()
    {
        var index = Random.Range(0, lightRockSounds.Length);
        audio.PlayOneShot(lightRockSounds[index]);
    }

    public void PlayRandomHeavyRockSound()
    {
        var index = Random.Range(0, heavyRockSounds.Length);
        audio.PlayOneShot(heavyRockSounds[index]);
    }

    public void PlayGemDrop()
    {
        audio.PlayOneShot(gemDrop);
    }

}
