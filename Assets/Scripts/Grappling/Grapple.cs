using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// MUST be attached to a separate empty object and not to a player
/// </summary>
public class Grapple : MonoBehaviour
{
    // used in Physics.Raycast
    private LineRenderer _lineRenderer;
    private Vector3 _grapplePoint;
    private Camera _camera;

    #region Grappling
    [Header("GRAPPLE")]
    [SerializeField]
    private LayerMask _grappleLayer;
    [SerializeField, Tooltip("point where the line renderer starts")]
    private Transform _gunTip;
    [SerializeField]
    private GameObject _player; // also used for referencing rigidbody
    [SerializeField, Tooltip("max distance that the player can grapple")]
    private float _maxDistance;
    [SerializeField, Range(0f, 1f)]
    private float _percentageDistance = .2f;
    [SerializeField]
    private float _speedDirection = 3f;

    #endregion



    #region Hookshot
    [Header("HOOKSHOT")]
    [SerializeField, Range(1f, 20f), Tooltip("Determines how fast the player goes towards the grapplepoint")]
    private float _maxHookShotSpeed = 5f;
    [SerializeField, Tooltip("acceleration when the player use the hookshot")]
    private float _accelerationMultiplier = 2f;


    #endregion

    #region private variables

    private float _speedHook = 0;
    private float _tetherLength; // length of the grapple
    private float _distanceToGrapplePull; // length of the player to grapple

   
    private bool _tethered; // bool variable that detects if the player has hit a grappable wall upon raycasting
    private bool _disableGrapple; 
    private bool _canPull; // Hookshot Bool
    private bool _isPulling; // Hookshot bool

    
    private Vector3 _tetherPoint; // position where the grapple got hit
    private Vector3 _grappleDirection;   
    private Vector3 _inputDirection;

    private Rigidbody _rb; // referencing from player's rb



    #endregion


    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _rb = _player.GetComponent<Rigidbody>();

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

    // Update is called once per frame
    void Update()
    {

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
                _canPull = true;

                _distanceToGrapplePull = _tetherLength * _percentageDistance;

                //_rb.AddForce(transform.forward * _hookShotSpeed, ForceMode.VelocityChange);
            }
        }else
        {
            StopGrapple();
        }
    }

    private void StopGrapple()
    {
        Player.movementState = MovementState.GroundMovement;
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
        

        if (Player.movementState == MovementState.Grappling)
        {
            
            //Acceleration here upon controls
            if (_inputDirection.z != 0 || _inputDirection.x != 0) 
            {
                // W [Forward input = faster acceleration]


                // S [Backward input = deceleration]
                Vector3 direction = _player.transform.right * -_inputDirection.x + _player.transform.forward * -_inputDirection.z;
            _rb.velocity -= speedTowardsGrapplePoint * directionToGrapple + direction;
            }


            //if (_tetherLength - currentDistanceToGrapple < _distanceToGrapplePull)
            //{
            //    _rb.MovePosition(_player.transform.position + directionToGrapple * _speedDirection * Time.deltaTime);

            //}

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

        _rb.isKinematic = true;
        float speedMultiplier = 2f;
        _speedHook += speedMultiplier * Time.deltaTime;
        _speedHook = Mathf.Clamp(_speedHook, 0, Vector3.Distance(_player.transform.position, _tetherPoint));
  

        _rb.useGravity = false;
        _rb.MovePosition(_player.transform.position + startHooking * _speedHook);


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
