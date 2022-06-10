using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SFX Volume Channel", menuName = "Channels/New SFX Volume Channel")]
    public class SFXVolumeChannel : ScriptableObject
    {
        public void ChangeSFXVolume(float volume)
        {
            SoundManager.Instance.OnChangeSFXVolume(volume);
        }
    }
}