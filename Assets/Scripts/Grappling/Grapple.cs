using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// MUST be attached to a separate empty object and not to a player
/// </summary>
public class Grapple : MonoBehaviour
{



    #region Grappling
    [Header("GRAPPLE")]
    [SerializeField]
    private LayerMask _grappleLayer;
    /// <summary>
    /// point where the line renderer starts
    /// </summary>
    [SerializeField, Tooltip("point where the line renderer starts")]
    private Transform _gunTip;
    /// <summary>
    /// // also used for referencing rigidbody and position
    /// </summary>
    [SerializeField]
    private GameObject _player; 

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


    #endregion

    #region Hookshot

    [Header("HOOKSHOT")]
    /// <summary>
    /// Determines how fast the player goes towards the grapplepoint
    /// </summary>
    [SerializeField, Range(1f, 20f), Tooltip("Determines how fast the player goes towards the grapplepoint")]
    private float _maxHookShotSpeed = 5f;
    /// <summary>
    /// acceleration when the player use the hookshot
    /// </summary>
    [SerializeField, Tooltip("acceleration when the player use the hookshot")]
    private float _accelerationMultiplier = 2f;


    #endregion

    #region private variables



    private LineRenderer _lineRenderer;

    private Camera _camera;

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
 
    private Vector3 _inputDirection;

    /// <summary>
    /// Referencing player's Rigidbody
    /// </summary>
    private Rigidbody _rb;

    private Player _p;


    #endregion


    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _rb = _player.GetComponent<Rigidbody>();
        _p = _player.GetComponent<Player>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _disableGrapple = true;
        _canPull = false;
    }

    private void OnEnable()
    {
        InputManager.onStartGrapple += StartGrapple;
        InputManager.onStartMovement += GetDirection;
        InputManager.onEndMovement += StopControlling;

        InputManager.onStartHook += StartHook;
        InputManager.onEndHook += StopHook;
    }
       
    private void OnDisable()
    {
        InputManager.onStartGrapple -= StartGrapple;
        InputManager.onStartMovement -= GetDirection;
        InputManager.onEndMovement -= StopControlling;

        InputManager.onStartHook -= StartHook;
        InputManager.onEndHook -= StopHook;
    }

    private void FixedUpdate()
    {
        if (_tethered)
        {
            ApplyGrapplePhysics();
        }

        if (_isPulling)
        {
            Debug.Log($"_isPulling: {_isPulling}");
          ApplyHookShotPhysics();
        }
    }


    private void LateUpdate()
    {
        DrawRope();
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
                Player.movementState = MovementState.Grappling;
               _tethered = true;
               _tetherPoint = hit.point;
               _tetherLength = Vector3.Distance(_tetherPoint, _player.transform.position);
                _initialLength = _tetherLength;
               _canPull = true;
            }
        }else
        {
            StopGrapple();
        }
    }

    private void StopGrapple()
    {
        Player.movementState = _p.onGround ? MovementState.GroundMovement : MovementState.OnAir;
        _disableGrapple = true;
        _tethered = false;
        _canPull = false;
        _lineRenderer.positionCount = 0;

    }

    // Rope Swing 
    private void ApplyGrapplePhysics()
    {
        Vector3 directionToGrapple = Vector3.Normalize(_tetherPoint - _player.transform.position);
        float currentDistanceToGrapple = Vector3.Distance(_tetherPoint, _player.transform.position);
        float speedTowardsGrapplePoint = Mathf.Round(Vector3.Dot(_rb.velocity, directionToGrapple) * 100) / 100;
        
        if (_p.onGround)
        {
            _tetherLength = Vector3.Distance(_tetherPoint, _player.transform.position);
            _tetherLength = Mathf.Clamp(_tetherLength, 0, _initialLength);
        }
        //Debug.Log($"{directionToGrapple}");
        if (Player.movementState == MovementState.Grappling)
        {
            //This is the part where the player can control the movement speed and direction while on grapple
            if ((_inputDirection.z != 0 || _inputDirection.x != 0 )) 
            {
                Vector3 direction = _player.transform.right * -_inputDirection.x + _player.transform.forward * -_inputDirection.z;
                _rb.velocity -= speedTowardsGrapplePoint * directionToGrapple + direction * _grappleSpeedMovementMultiplier;
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
    }

    #endregion


    private void DrawRope()
    {
        if (_tethered)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, _gunTip.position);
            _lineRenderer.SetPosition(1, _tetherPoint);

        }

    }

    #region HOOKSHOT

    private void StartHook()
    {
        if (!_canPull) return;

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

        Vector3 startHooking = Vector3.Normalize(_tetherPoint - _player.transform.position);

        float speedMultiplier = 2f;
        _speedHook += speedMultiplier * Time.deltaTime;
        _speedHook = Mathf.Clamp(_speedHook, 0, Vector3.Distance(_player.transform.position, _tetherPoint));


        if (_inputDirection.z != 0 || _inputDirection.x != 0)
        {
            Vector3 direction = _player.transform.right * -_inputDirection.x + _player.transform.forward * -_inputDirection.z;
            _rb.velocity -= _speedHook * startHooking + direction;
        }

        _rb.velocity += startHooking * _speedHook;

    }

    #endregion

    #region Player Input
    private void GetDirection(Vector2 direction)
    {
        _inputDirection = new Vector3(direction.x, _player.transform.position.y, direction.y);
    }

    private void StopControlling()
    {
        _inputDirection.x = 0;
        _inputDirection.z = 0;
    }
    #endregion

}
