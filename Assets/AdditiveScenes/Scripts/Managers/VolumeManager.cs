using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace AdditiveScenes.Scripts.Managers
{
    public class VolumeManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider bgmVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        [Header("Channels")] 
        [SerializeField] private BGMVolumeChannel bgmVolumeChannel;
        [SerializeField] private SFXVolumeChannel sfxVolumeChannel;

        private void OnEnable()
        {
            bgmVolumeSlider.value = bgmVolumeChannel.GetVolume;
            sfxVolumeSlider.value = sfxVolumeChannel.GetVolume;
        }
    }
}
