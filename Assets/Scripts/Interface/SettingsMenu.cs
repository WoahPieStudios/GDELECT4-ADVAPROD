using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public AudioMixer audioMixer2;

   public void SetMasterVolume(float MasterVol)
    {
        audioMixer.SetFloat("masterVolume", MasterVol);
    }

    public void SetSFXVolume(float SFXVol)
    {
        audioMixer2.SetFloat("SFXVolume", SFXVol);
    }

    public void SetQualityLow (bool isLow)
    {
        QualitySettings.SetQualityLevel(0, isLow);
        Debug.Log("SetQualityLow");
    }

    public void SetQualityMedium(bool isMedium)
    {
        QualitySettings.SetQualityLevel(1, isMedium);
    }

    public void SetQualityHigh(bool isHigh)
    {
        QualitySettings.SetQualityLevel(2, isHigh);
    }
}
