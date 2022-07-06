using UnityEngine;
using UnityEngine.Events;
public abstract class TutorialEventChannel : ScriptableObject
{
    [SerializeField] protected UnityEvent onTutorialEvent;
    public abstract void OnTutorialEvent();
}
