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


    [SerializeField]
    private LayerMask _grappleLayer;
    [SerializeField]
    private GameObject _player; // also used for referencing rigidbody
    [SerializeField, Range(10f, 100f), Tooltip("max distance that the player can grapple")]
    private float _maxDistance;
    [SerializeField, Range(1f, 10f), Tooltip("reduces speed during grappling via Player Controls")]
    private float _cancelSpeed;

    #region Hookshot
    [Header("HOOKSHOT")]
    [SerializeField, Range(1f, 20f), Tooltip("Determines how fast the player goes towards the grapplepoint")]
    private float _hookShotSpeed = 5f;

    private float _speedHook = 0;

    #endregion

    #region private variables

    private bool _tethered;
    private bool _disableGrapple;
    private float _tetherLength;
    private Vector3 _tetherPoint;
    private Vector3 _grappleDirection;
    private Rigidbody _rb;


    private bool _canPull; // 
    private bool _isPulling;


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
        //InputManager.onEndGrapple += StopGrapple;

        InputManager.onStartHook += StartHook;
        InputManager.onEndHook += StopHook;
    }
       
    private void OnDisable()
    {
        InputManager.onStartGrapple -= StartGrapple;
       // InputManager.onEndGrapple -= StopGrapple;

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
                Debug.Log($"_tetherLength: {_tetherLength} units");
                _canPull = true;
                //_rb.AddForce(transform.forward * _hookShotSpeed, ForceMode.VelocityChange);
            }
        }else
        {
            StopGrapple();
        }
    }

    private void StopGrapple()
    {
        _disableGrapple = true;
        _tethered = false;
        _canPull = false;
        Player.movementState = MovementState.GroundMovement;
        _lineRenderer.positionCount = 0;
    }
    private void ApplyGrapplePhysics()
    {
        Vector3 directionToGrapple = Vector3.Normalize(_tetherPoint - _player.transform.position);
        float currentDistanceToGrapple = Vector3.Distance(_tetherPoint, _player.transform.position);
        float speedTowardsGrapplePoint = Mathf.Round(Vector3.Dot(_rb.velocity, directionToGrapple) * 100) / 100;



        //_player.transform.position += directionToGrapple * _hookShotSpeed * Time.deltaTime;
        
        if (speedTowardsGrapplePoint < 0)
        {
            //if currentDistanceToGrapple is greater than the _tetherLength, velocity must cancel out and
            // must go to the opposite direction
            if (currentDistanceToGrapple > _tetherLength)     
            {
                _rb.velocity -= speedTowardsGrapplePoint * directionToGrapple; // this makes the player swing to the opposite side
                _rb.position = _tetherPoint - directionToGrapple; // this makes the player's direction to swing go to the opposite side
            }
        }
    }

    #endregion


    private void DrawRope()
    {
        if (_tethered)
        {
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
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
        //_rb.velocity += startHooking * _speedHook;


    }

    #endregion

}
