using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Channels/Audio/New Random SFX Channel")]
    public class RandomSFXChannel : ScriptableObject
    {
        [SerializeField] private AudioClip[] audioClips;

        public void PlayAudio()
        {
            SoundManager.Instance.OnPlaySFX(audioClips[Random.Range(0, audioClips.Length)]);
        }

        public void PlayAudio(AudioSource source)
        {
            SoundManager.Instance.OnPlayAtSource(audioClips[Random.Range(0, audioClips.Length)], source);
        }
    }
}