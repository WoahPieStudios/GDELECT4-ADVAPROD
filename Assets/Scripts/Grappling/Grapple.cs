using System;
using System.Collections;
using System.Collections.Generic;
using Spawning.Scripts.Managers;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using System.Threading.Tasks;
using Handlers;

/// <summary>
/// MUST be attached to a separate empty object and not to a player
/// </summary>
public class Grapple : MonoBehaviour
{

    #region SFX
    [SerializeField]
    private SFXChannel _grappleLaunchChannel;
    [SerializeField]
    private SFXChannel _grappleHookedChannel;
    [SerializeField]
    private SFXChannel _grapplePullChannel;
    [SerializeField]
    private SFXChannel _grappleReleaseChannel;
    #endregion

    #region VFX
    [Header("VFX")] 
    [SerializeField] private VFXHandler dustHitEffect;
    #endregion
    
    #region PARTICLE FX
    [Header("Particle System")]
    [SerializeField]
    private ParticleSystem _grappleSpeedLines;
    [SerializeField, Range(1f, 20f), Tooltip("threshold for the speed for the speed lines to stop showing")]
    private float _speedThreshold = 5f;
    #endregion

    #region Grappling
    [Header("GRAPPLE")]
    [SerializeField]
    private LayerMask _grappleLayer;
    [SerializeField]
    private LayerMask _ungrappables;
    /// <summary>
    /// // also used for referencing rigidbody and position
    /// </summary>
    [SerializeField]
    private GameObject _player;

    [SerializeField, Range(0,45)]
    private int _angleToDisconnect;
    [SerializeField, Range(0,20)]
    private float _minHeightToAutoPull;

    /// <summary>
    /// max distance that the player can grapple
    /// </summary>
    [SerializeField, Tooltip("max distance that the player can grapple")]
    private float _maxDistance;

    /// <summary>
    /// Speed Multiplier for Player Movement while grappling (Multiplies direction value which is 1)
    /// </summary>
    [SerializeField, Tooltip("Speed Multiplier for Player Movement while grappling (Multiplies direction value which is 1)")]
    private float _grappleSpeedMovementMultiplier = 1f;

    [SerializeField, Range(0f,1f)]
    private float _delayAutoDisconnetinms = 0.5f;
    #endregion

    #region Hookshot

    [Header("HOOKSHOT")]
    /// <summary>
    /// Determines how fast the player goes towards the grapplepoint
    /// </summary>
    [SerializeField, Tooltip("Determines how fast the player goes towards the grapplepoint")]
    private float _maxHookShotSpeed = 5f;
    /// <summary>
    /// acceleration when the player use the hookshot
    /// </summary>
    [SerializeField, Tooltip("acceleration when the player use the hookshot")]
    private float _accelerationMultiplier = 2f;
    /// <summary>
    /// Minimum distance towards the grapple point before it automatically releases
    /// </summary>
    [SerializeField, Tooltip("Minimum distance towards the grapple point before it automatically releases")]
    private float _minDistanceToGrapplePoint;

    [SerializeField, Range(0.01f, 100f)]
    private float _percentLengthToSlowDown;

    #endregion


    #region private variables

    private Camera _camera;

    private bool _canCheck;
    private float _speedHook = 0;

    /// <summary>
    /// Length of the grapple
    /// </summary>
    private float _tetherLength;
    /// <summary>
    /// Length of grapple can be changed due to circumstances so the initial length should be stored to give a maximum extension
    /// </summary>
    private float _initialLength;

    /// <summary>
    /// detects if the player has hit a grappable wall upon raycasting
    /// </summary>
    private bool _tethered; 

    public bool tethered
    {
        get => _tethered;
        set => _tethered = value;
    }

    /// <summary>
    /// relies on player input if the grapple should be disabled or enabled
    /// </summary>
    private bool _disableGrapple;

    /// <summary>
    /// determines if the player has grappled onto something and can be pulled towards the grapple point
    /// </summary>
    private bool _canPull;

    /// <summary>
    /// determines if the player is holding the HookShot button
    /// </summary>
    private bool _isPulling;

    /// <summary>
    /// point where the raycast has been hit
    /// </summary>
    private Vector3 _tetherPoint;

    public Vector3 tetherPoint
    {
        get => _tetherPoint;
        set => _tetherPoint = value;
    } 
 
    private Vector3 _inputDirection;

    /// <summary>
    /// Referencing player's Rigidbody
    /// </summary>
    private Rigidbody _rb;
    private Player _p;


    #endregion

    private int _crosshairIndex;

    private void Awake()
    {
        _rb = _player.GetComponent<Rigidbody>();
        _p = _player.GetComponent<Player>();

    }
    
    void Start()
    {
        _tethered = false;
        _camera = Camera.main;
        _disableGrapple = true;
        _canPull = false;
        _grappleSpeedLines.Stop();
    }

    private void OnEnable()
    {
        InputManager.onStartGrapple += StartGrapple;
        InputManager.onStartMovement += GetInputDirection;
        InputManager.onEndMovement += StopControlling;

        InputManager.onStartHook += StartHook;
        InputManager.onEndHook += StopHook;

        PlayerSpawnManager.RespawnPlayer += StopGrapple;

    }
       
    private void OnDisable()
    {
        InputManager.onStartGrapple -= StartGrapple;
        InputManager.onStartMovement -= GetInputDirection;
        InputManager.onEndMovement -= StopControlling;

        InputManager.onStartHook -= StartHook;
        InputManager.onEndHook -= StopHook;
        PlayerSpawnManager.RespawnPlayer -= StopGrapple;
    }

    private void Update()
    {
        if (Player.movementState == MovementState.Grappling)
        {
            if (_rb.velocity.magnitude > _speedThreshold)
            {
                //play vfx
                _grappleSpeedLines.Play();
            }else
            {
                _grappleSpeedLines.Stop();
            }
        }
        else
        {
            _grappleSpeedLines.Stop();
        }

    }

    private void FixedUpdate()
    {

        if (_tethered)
        {
            ApplyGrapplePhysics();
        }

        if (!_tethered && !_p.onGround)
        {
            Player.movementState = MovementState.OnAir;
        }

        if (_isPulling)
        {   
          ApplyHookShotPhysics();
        }

        GrappleCrosshair.OnUpdateGrappleCH(HitType());

    }

    private int HitType()
    {
        RaycastHit hit;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _maxDistance, _grappleLayer))
        {
            return 1;
            
        }
        else if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _maxDistance, _ungrappables))
        {
            if (!hit.collider.CompareTag("Enemy")) return 2;
        }else
        {
            return 0;
        }
        return _crosshairIndex;

    }


    #region GRAPPLING
    private void StartGrapple()
    {
        RaycastHit hit;
        _disableGrapple = !_disableGrapple;
        //made grappling so that instead of holding the grapple button, player will just press again to release
        if (!_disableGrapple)
        {
            if (Physics.Raycast(_camera.transform.position,_camera.transform.forward, out hit, _maxDistance, _grappleLayer))
            {
                _grappleHookedChannel?.PlayAudio();
                Instantiate(dustHitEffect, _tetherPoint, Quaternion.identity);
                Player.movementState = MovementState.Grappling;
                _tethered = true;
                _tetherPoint = hit.point;
                _tetherLength = Vector3.Distance(_tetherPoint, _player.transform.position);
                AutoDisconnectDelay();
                _initialLength = _tetherLength;
                _canPull = true;
                if (_player.transform.position.y + _minHeightToAutoPull > _tetherPoint.y)
                {
                    StartHook();
                }

            }
        }else
        {
            StopGrapple();
        }
    }


    private void StopGrapple()
    {
        //insert release sound
        _grappleReleaseChannel?.PlayAudio();
        Player.movementState = _p.onGround ? MovementState.GroundMovement : MovementState.OnAir;
        _disableGrapple = true;
        _tethered = false;
        _canPull = false;
        _isPulling = false;
        _canCheck = false;

    }


    private async void AutoDisconnectDelay()
    {
        var current = Time.time + _delayAutoDisconnetinms;
        _canCheck = false;
        while (Time.time < current)
        {
            await Task.Yield();
        }
        //insert here
        _canCheck = _tethered ? true : false;
    }

    // Rope Swing 
    private void ApplyGrapplePhysics()
    {
        Vector3 directionToGrapple = GetDirection();
        float currentDistanceToGrapple = Vector3.Distance(_tetherPoint, _player.transform.position);
        float speedTowardsGrapplePoint = Mathf.Round(Vector3.Dot(_rb.velocity, directionToGrapple) * 100) / 100;
        

        if (_p.onGround)
        {
            _tetherLength = Vector3.Distance(_tetherPoint, _player.transform.position);
            _tetherLength = Mathf.Clamp(_tetherLength, 0, _initialLength);
        }

        if (Player.movementState == MovementState.Grappling)
        {
            //This is the part where the player can control the movement speed and direction while on grapple
            if ((_inputDirection.z != 0 || _inputDirection.x != 0 ) && !_isPulling) 
            {

                Vector3 direction = _player.transform.right * _inputDirection.x + _player.transform.forward * _inputDirection.z;
                _rb.velocity += direction * _grappleSpeedMovementMultiplier;
                _rb.position = _tetherPoint - directionToGrapple * _tetherLength;
            }
        }
        

        
        if (speedTowardsGrapplePoint < 0)
        {
            //if currentDistanceToGrapple is greater than the _tetherLength, velocity must cancel out and
            // must go to the opposite direction
            if (currentDistanceToGrapple > _tetherLength)     
            {
                _rb.velocity -= speedTowardsGrapplePoint * directionToGrapple; // this makes the player swing to the opposite side
                _rb.position = _tetherPoint - directionToGrapple * _tetherLength; // this makes the player's direction to swing go to the opposite side
            }
        }
        Debug.Log($"Angle is {Vector3.SignedAngle(_tetherPoint, directionToGrapple, Vector3.forward)}");

        if (!_canCheck) return;

        float angle = Vector3.SignedAngle(_tetherPoint, directionToGrapple, Vector3.forward);
        if (angle < -_angleToDisconnect)
        {
            StopGrapple();
        }


    }

    #endregion



    #region HOOKSHOT

    private void StartHook()
    {
        if (!_canPull) return;
        // hinihila si player pull audio
        _grapplePullChannel?.PlayAudio();
        _isPulling = true;


    }

    private void StopHook()
    {
        _isPulling = false;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        _speedHook = 0;
        StopGrapple();
    }

    
    private void ApplyHookShotPhysics()
    {

        Vector3 startHooking = GetDirection();

        _speedHook += _accelerationMultiplier * Time.deltaTime;
        _speedHook = Mathf.Clamp(_speedHook, 0, Vector3.Distance(_player.transform.position, _tetherPoint));

        _rb.velocity += startHooking * _speedHook;
        // Automatically disables HookShot upon reaching a certain distance 
        if (Vector3.Distance(_tetherPoint , _player.transform.position) <= _minDistanceToGrapplePoint)
        {
            _rb.velocity = new Vector3 (_rb.velocity.x, _rb.velocity.y + 5f, _rb.velocity.z);
            StopHook();
        }
    }

    #endregion

    #region Player Input
    private void GetInputDirection(Vector2 direction)
    {
        _inputDirection = new Vector3(direction.x, _player.transform.position.y, direction.y);
    }

    private void StopControlling()
    {
        _inputDirection.x = 0;
        _inputDirection.z = 0;
    }
    #endregion


    private Vector3 GetDirection()
    {
        return Vector3.Normalize(_tetherPoint - _player.transform.position);
    }
}
