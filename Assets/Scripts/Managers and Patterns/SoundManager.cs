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
    private event Action<float> onSetBGMVolume, onSetSFXVolume;

    private void OnEnable()
    {
        onPlaySFX += PlaySFX;
        onSetBGMVolume += SetBGMVolume;
        onSetSFXVolume += SetSFXVolume;
    }

    public float GetBGMVolume => musicSource.volume;
    public float GetSFXVolume => sfxSource.volume;
    
    private void SetBGMVolume(float volume)
    {
        musicSource.volume = volume;
    }
    
    private void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    
    private void OnDisable()
    {
        onPlaySFX -= PlaySFX;
        onSetBGMVolume -= SetBGMVolume;
        onSetSFXVolume -= SetSFXVolume;
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

    public void OnSetBGMVolume(float volume)
    {
        onSetBGMVolume?.Invoke(volume);
    }
    
    public void OnSetSFXVolume(float volume)
    {
        onSetSFXVolume?.Invoke(volume);
    }
}
