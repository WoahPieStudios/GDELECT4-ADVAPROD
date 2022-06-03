using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Events
    #region Player Movements & Actions
    public static event Action<Vector2> onStartMovement;
    public static event Action onEndMovement;

    public static event Action onStartGrapple;
    public static event Action onEndGrapple;

    public static event Action onStartHook;
    public static event Action onEndHook;

    public static event Action<Vector2> onMouseLook;

    public static event Action onShoot;
    #endregion

    #region UI Interactions
    public static event Action onPause; 
    public static event Action onResume;
    #endregion
    #endregion

    private PlayerInputs _playerInputs;
    private Vector2 _mouseInput;
    public AudioClip buttonSound;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        
    }
    private void Start()
    {
       

        #region Grappling & Hooking
        _playerInputs.PlayerControls.Grapple.performed += ctx =>
        {
            Debug.Log("Grappling!");
            onStartGrapple?.Invoke();
        };
        _playerInputs.PlayerControls.Grapple.canceled += ctx =>
        {
            Debug.Log("Stopped Grappling");
            onEndGrapple?.Invoke();
        };

        _playerInputs.PlayerControls.Sprint.performed += ctx =>
        {
            Debug.Log("Hooking forward");
            onStartHook?.Invoke();
        };
        _playerInputs.PlayerControls.Sprint.canceled += ctx =>
        {
            Debug.Log("Hook done");
            onEndHook?.Invoke();
        }; 

        #endregion

        #region GroundMovement
        _playerInputs.PlayerControls.Movement.performed += axis =>
        {
            Vector2 direction = axis.ReadValue<Vector2>();
            onStartMovement?.Invoke(direction);
        };
        _playerInputs.PlayerControls.Movement.canceled += axis =>
        {
            //Vector2 direction = Vector2.zero;
            //onStartMovement?.Invoke(direction);
            onEndMovement?.Invoke();
        };
        #endregion


        #region Shooting
        _playerInputs.PlayerControls.Shoot.performed += ctx =>
        {
            onShoot?.Invoke();
        };
        #endregion

        #region Mouse Look
        _playerInputs.PlayerControls.LookX.performed += ctx =>
        {
            if (!PauseMenu.isPaused)
            _mouseInput.x = ctx.ReadValue<float>();
            onMouseLook?.Invoke(_mouseInput);
        };
        _playerInputs.PlayerControls.LookY.performed += ctx =>
        {
            _mouseInput.y = ctx.ReadValue<float>();
            onMouseLook?.Invoke(_mouseInput);
        };
        #endregion
        
        #region UI Interaction

        #region Pause
        
        _playerInputs.UIInteraction.Pause.performed += ctx =>
        {
            //Subscribe to onPause for things that could happen during pause
            onPause?.Invoke();
        };


        #endregion


        #endregion
    }

    private void AudioTest(InputAction.CallbackContext obj) 
    {
       
            Debug.Log("Space is pressed");
            SoundManager.instance.PlaySFX(buttonSound);
       
    }


    private void OnEnable()
    {
        _playerInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
    }

}
