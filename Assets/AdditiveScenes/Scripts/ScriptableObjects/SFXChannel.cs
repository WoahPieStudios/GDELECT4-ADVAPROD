using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SFX Channel", menuName = "Channels/New SFX Channel")]
    public class SFXChannel : ScriptableObject
    {
        [SerializeField] private AudioClip audioClip;
        
        public void PlayAudio()
        {
            SoundManager.Instance.OnPlaySFX(audioClip);
        }
    }
}