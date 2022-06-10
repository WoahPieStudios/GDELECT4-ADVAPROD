using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource musicSource, sfxSource;

    private event Action<AudioClip> onPlaySFX;
    private event Action<float> onChangeBGMVolume, onChangeSFXVolume;

    private void OnEnable()
    {
        onPlaySFX += PlaySFX;
        onChangeBGMVolume += ChangeBGMVolume;
        onChangeSFXVolume += ChangeSFXVolume;
    }

    private void ChangeBGMVolume(float volume)
    {
        musicSource.volume = volume;
    }
    
    private void ChangeSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    
    private void OnDisable()
    {
        onPlaySFX -= PlaySFX;
        onChangeBGMVolume -= ChangeBGMVolume;
        onChangeSFXVolume -= ChangeSFXVolume;
    }

    public void PlayAudio(AudioSource source, AudioClip clip, float volume)
    {
        source.PlayOneShot(clip);
        source.volume = volume;
    }

    private void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        //sfxSource.volume = 1f;
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
        musicSource.volume = 1f;
    }

    public void OnPlaySFX(AudioClip clip)
    {
        onPlaySFX?.Invoke(clip);
    }

    public void OnChangeBGMVolume(float volume)
    {
        onChangeBGMVolume?.Invoke(volume);
    }
    
    public void OnChangeSFXVolume(float volume)
    {
        onChangeSFXVolume?.Invoke(volume);
    }
}
