using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEventsControls : MonoBehaviour
{
    private bool _checker;

    private void OnEnable()
    {
        InputManager.onPause += Testing; // subscribe from the event

    }

    private void OnDisable()
    {
        InputManager.onPause -= Testing; // unsubscribe from the event 
    }

    // lahat ng nakalagay sa loob ng if statement sa Input.GetKeyDown(KeyCode.Escape)
    private void Testing()
    {
        _checker = !_checker; // nagsiswitch lang toh ng on at off (true or false)


    }
}
