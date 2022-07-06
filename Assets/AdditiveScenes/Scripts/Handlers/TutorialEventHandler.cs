using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialEventHandler : MonoBehaviour
{
    [SerializeField] UnityEvent onTutorialEvent;

    public void OnTutorialEvent()
    {
        onTutorialEvent?.Invoke();
    }

}
