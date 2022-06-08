using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    
    public enum GraphicsSettings{
        Low, Medium, High
    }

    public GraphicsSettings selectedGraphicsSetting;

    public AudioSource mainSource;
    
    public AudioMixer audioMixer;

    public AudioMixer audioMixer2;

    private void Awake()
    {
        SetQualityLow(true);
    }

    public void SetMasterVolume(float MasterVol)
    {
        mainSource.volume = MasterVol;
    }

    public void SetSFXVolume(float SFXVol)
    {
        audioMixer2.SetFloat("SFXVolume", SFXVol);
    }

    public void SetQualityLow (bool isLow)
    {
        selectedGraphicsSetting = GraphicsSettings.Low;
        QualitySettings.SetQualityLevel(0, isLow);
        Debug.Log("SetQualityLow");
    }

    public void SetQualityMedium(bool isMedium)
    {
        selectedGraphicsSetting = GraphicsSettings.Medium;
        QualitySettings.SetQualityLevel(1, isMedium);
    }

    public void SetQualityHigh(bool isHigh)
    {
        selectedGraphicsSetting = GraphicsSettings.High;
        QualitySettings.SetQualityLevel(2, isHigh);
    }
}
