using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Tutorial.Scripts.Managers
{
    public class TutorialGameManager : MonoBehaviour
    {
        [SerializeField] private PauseEventChannel pauseEventChannel;
        [SerializeField] private UnityEvent onGameStart, onGamePause, onGameResume, onGameOver;

        private void OnEnable()
        {
            InputManager.onPause += OnGamePause;
            SceneManager.sceneLoaded += (arg0, mode) => { OnGameStart(); };
        }

        public void OnGameStart()
        {
            onGameStart?.Invoke();
        }

        public void OnGamePause()
        {
            onGamePause?.Invoke();
        }
        
        public void OnGameResume()
        {
            onGameResume?.Invoke();
        }
        
        public void OnGameOver()
        {
            onGameOver?.Invoke();
        }
    }
}
