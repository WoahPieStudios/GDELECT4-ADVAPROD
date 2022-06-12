using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SFX Volume Channel", menuName = "Channels/Audio/New SFX Volume Channel")]
    public class SFXVolumeChannel : ScriptableObject
    {
        public float GetVolume => SoundManager.Instance.GetSFXVolume;
        public void SetVolume(float volume)
        {
            SoundManager.Instance.OnSetSFXVolume(volume);
        }
    }
}