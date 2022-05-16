using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    public AudioSource musicSource, sfxSource;
    //public AudioClip clip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void PlayAudio(AudioSource source, AudioClip clip, float volume)
    {
        source.PlayOneShot(clip);
        source.volume = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
        sfxSource.volume = 0.3f;
    }

    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
        musicSource.volume = 0.1f;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
            
    }

}