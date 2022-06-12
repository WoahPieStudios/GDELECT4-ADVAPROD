using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BGM Volume Channel", menuName = "Channels/Audio/New BGM Volume Channel")]
    public class BGMVolumeChannel : ScriptableObject
    {
        public float GetVolume => SoundManager.Instance.GetBGMVolume;
        public void SetVolume(float volume)
        {
            SoundManager.Instance.OnSetBGMVolume(volume);
        }
    }
}