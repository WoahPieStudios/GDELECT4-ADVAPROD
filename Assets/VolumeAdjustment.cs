using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjustment : MonoBehaviour
{
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private Slider main, sfx;

    private void OnEnable()
    {
        main.value = settingsMenu.mainSource.volume;
    }
}
