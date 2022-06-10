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

    private void OnEnable()
    {
        onPlaySFX += PlaySFX;
    }

    private void OnDisable()
    {
        onPlaySFX -= PlaySFX;
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
}
