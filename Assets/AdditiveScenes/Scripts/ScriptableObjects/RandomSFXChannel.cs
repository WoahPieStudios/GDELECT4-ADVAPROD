using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Channels/Audio/New Random SFX Channel")]
    public class RandomSFXChannel : ScriptableObject
    {
        [SerializeField] private AudioClip[] audioClips;

        public void PlayAudio()
        {
            var clip = audioClips[Random.Range(0, audioClips.Length)];
            SoundManager.Instance.OnPlaySFX(clip);
        }

        public void PlayAudio(AudioSource source)
        {
            var clip = audioClips[Random.Range(0, audioClips.Length)];
            SoundManager.Instance.OnPlayAtSource(clip, source);
        }
    }
}