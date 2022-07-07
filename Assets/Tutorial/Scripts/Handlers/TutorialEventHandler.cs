using System;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial.Scripts.Handlers
{
    public class TutorialEventHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTutorialEvent;
        public virtual void OnTutorialEvent() => onTutorialEvent?.Invoke();

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                OnTutorialEvent();
        }
    }
}
