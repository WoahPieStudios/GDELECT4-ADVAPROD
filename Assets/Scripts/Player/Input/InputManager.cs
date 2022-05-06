using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private PlayerInputs _playerInputs;

    private void Start()
    {
        _playerInputs = new PlayerInputs();


        _playerInputs.PlayerControls.Grapple.performed += EnableGrapple;
    }


    private void OnEnable()
    {
        _playerInputs.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.PlayerControls.Disable();
    }
    private void EnableGrapple(InputAction.CallbackContext obj)
    {
        
    }

}
