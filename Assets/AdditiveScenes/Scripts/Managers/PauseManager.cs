using System;
using UnityEngine;

namespace AdditiveScenes.Scripts.Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static event Action onPause,onResume;
        public static bool canUsePauseUI { get; private set; }

        private void OnEnable()
        {
            onPause += () => { Time.timeScale = 0f; };
            onResume += () => { Time.timeScale = 1f; };
        }

        public static void SetPauseUIAvailability(bool canUsePause)
        {
            canUsePauseUI = canUsePause;
            print($"canUsePause: {canUsePauseUI}");
        }
        
        public static void OnPause()
        {
            onPause?.Invoke();
        }

        public static void OnResume()
        {
            onResume?.Invoke();
        }
    }
}