using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCaller : MonoBehaviour
{
    public void PlaySFX(AudioClip clip)
    {
        SoundManager.instance.PlaySFX(clip);
        

    }
}
