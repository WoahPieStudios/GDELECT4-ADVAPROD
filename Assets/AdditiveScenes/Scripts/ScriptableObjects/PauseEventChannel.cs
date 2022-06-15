using System;
using AdditiveScenes.Scripts.Managers;
using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Pause Event Channel", menuName = "Channels/Gameplay/New Pause Event Channel")]
    public class PauseEventChannel : ScriptableObject
    {
        public bool canUseUI => PauseManager.canUsePauseUI;
        
        public void SetUseUI(bool canUse)
        {
            PauseManager.SetPauseUIAvailability(canUse);
        }
        
        public void AddPauseListener(Action listener)
        {
            PauseManager.onPause += listener;
        }

        public void RemovePauseListener(Action listener)
        {
            PauseManager.onPause += listener;
        }

        public void AddResumeListener(Action listener)
        {
            PauseManager.onResume += listener;
        }

        public void RemoveResumeListener(Action listener)
        {
            PauseManager.onResume -= listener;
        }

        public void OnPause()
        {
            PauseManager.OnPause();
        }

        public void OnResume()
        {
            PauseManager.OnResume();
        }
    }
}