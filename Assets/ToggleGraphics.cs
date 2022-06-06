using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleGraphics : MonoBehaviour
{
    [SerializeField] private Toggle low, med, high;
    [SerializeField] private SettingsMenu settings;
    
    private void OnEnable()
    {
        switch (settings.selectedGraphicsSetting)
        {
            case SettingsMenu.GraphicsSettings.Low:
                low.isOn = true;
                med.isOn = false;
                high.isOn = false;
                break;
            case SettingsMenu.GraphicsSettings.Medium:
                low.isOn = false;
                med.isOn = true;
                high.isOn = false;
                break;
            case SettingsMenu.GraphicsSettings.High:
                low.isOn = false;
                med.isOn = false;
                high.isOn = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }    
    }
}
