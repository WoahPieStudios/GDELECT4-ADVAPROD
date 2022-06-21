using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdditiveScenes.Scripts.Managers
{
    public class BGMManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioClip[] tracks;
        private event Action<AudioClip> onTrackFinished;

        private void Start()
        {
            StartPlayback(tracks[Random.Range(0, tracks.Length)]);
        }

        private void OnEnable()
        {
            onTrackFinished += StartPlayback;
        }

        private void OnDisable()
        {
            onTrackFinished -= StartPlayback;
        }

        private void StartPlayback(AudioClip selectedTrack)
        {
            StartCoroutine(PlaybackRoutine(selectedTrack));
        }
        
        private IEnumerator PlaybackRoutine(AudioClip selectedTrack)
        {
            bgmSource.clip = selectedTrack;
            bgmSource.Play();
            yield return new WaitForSecondsRealtime(selectedTrack.length);
            selectedTrack = tracks[Random.Range(0, tracks.Length)];
            onTrackFinished?.Invoke(selectedTrack);
        }
    }
}
