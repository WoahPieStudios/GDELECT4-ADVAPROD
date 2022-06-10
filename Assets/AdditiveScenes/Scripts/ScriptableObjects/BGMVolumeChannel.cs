using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BGM Volume Channel", menuName = "Channels/New BGM Volume Channel")]
    public class BGMVolumeChannel : ScriptableObject
    {
        public void ChangeBGMVolume(float volume)
        {
            SoundManager.Instance.OnChangeBGMVolume(volume);
        }
    }
}