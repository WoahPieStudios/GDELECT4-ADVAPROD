using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject ObjectMusic;

    public GameObject ObjectSfx;

    public AudioSource mainSource;

    public AudioSource sfxSource;
    
    public AudioMixer audioMixer;

    public AudioMixer audioMixer2;

    public Slider masterSlider;

    public Slider sfxSlider;

    private void Start()
    {
        ObjectMusic = GameObject.FindWithTag("GameMusic");
        mainSource = ObjectMusic.GetComponent<AudioSource>();
        ObjectSfx = GameObject.FindWithTag("GameSFX");
        sfxSource = ObjectSfx.GetComponent<AudioSource>();
        mainSource.volume = PlayerPrefs.GetFloat("MasterVol");
        sfxSource.volume = PlayerPrefs.GetFloat("SfxVol");
        masterSlider.value = mainSource.volume;
        sfxSlider.value = sfxSource.volume;
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("MasterVol", mainSource.volume);
        PlayerPrefs.SetFloat("SfxVol", sfxSource.volume);
    }

    public void SetMasterVolume(float MasterVol)
    {
        mainSource.volume = MasterVol;
    }

    public void SetSFXVolume(float SfxVol)
    {
        sfxSource.volume = SfxVol;
    }
}
