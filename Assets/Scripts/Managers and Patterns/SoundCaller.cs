using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaller : MonoBehaviour
{

    public AudioClip bgm;


    public void PlaySFX(AudioClip clip)
    {
        SoundManager.instance.PlaySFX(clip);
        
    }

    public void PlayMusic(AudioClip music)
    {
        SoundManager.instance.PlayMusic(music);
    }
}
