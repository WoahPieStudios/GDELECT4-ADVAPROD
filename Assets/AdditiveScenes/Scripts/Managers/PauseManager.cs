using System;
using UnityEngine;

namespace AdditiveScenes.Scripts.Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static event Action onPause,onResume;

        private void OnEnable()
        {
            onPause += () => { Time.timeScale = 0f; };
            onResume += () => { Time.timeScale = 1f; };
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