using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events
    public static event Action<Vector2> onStartMovement;
    public static event Action onEndMovement;

    public static event Action onStartGrapple;
    public static event Action onEndGrapple;

    public static event Action<Vector2> onMouseLook;
    #endregion

    private PlayerInputs _playerInputs;
    private Vector2 _mouseInput;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        
    }
    private void Start()
    {


        _playerInputs.PlayerControls.Grapple.performed += EnableGrapple;
        _playerInputs.PlayerControls.Grapple.canceled += DisableGrapple;

        _playerInputs.PlayerControls.Movement.performed += EnableMovement;
        _playerInputs.PlayerControls.Movement.canceled += DisableMovement;

        #region Mouse Look
        _playerInputs.PlayerControls.LookX.performed += ctx =>
        {
            _mouseInput.x = ctx.ReadValue<float>();
            onMouseLook?.Invoke(_mouseInput);
        };
        _playerInputs.PlayerControls.LookY.performed += ctx =>
        {
            _mouseInput.y = ctx.ReadValue<float>();
            onMouseLook?.Invoke(_mouseInput);
        };
        #endregion
    }


    private void OnEnable()
    {
        _playerInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
    }

    #region Grappling
    private void EnableGrapple(InputAction.CallbackContext obj)
    {
        Debug.Log("Grappling!");
        onStartGrapple?.Invoke();
    }
    private void DisableGrapple(InputAction.CallbackContext obj)
    {
        Debug.Log("Stopped Grappling");
        onEndGrapple?.Invoke();
    }
    #endregion 

    #region Movement
    private void DisableMovement(InputAction.CallbackContext obj)
    {
        Vector2 direction = Vector2.zero;
        onStartMovement?.Invoke(direction);
    }

    private void EnableMovement(InputAction.CallbackContext obj)
    {
        Vector2 direction = obj.ReadValue<Vector2>();
        onStartMovement?.Invoke(direction);
    }

    #endregion

}
