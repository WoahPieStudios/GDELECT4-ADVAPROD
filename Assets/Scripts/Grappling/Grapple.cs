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
    private GameObject _player;
    [SerializeField, Tooltip("max distance that the player can grapple"), Range(10f, 100f)]
    private float _maxDistance;

    #region Grappling variables

    private bool _tethered;
    private bool _disableGrapple;
    private float _tetherLength;
    private Vector3 _tetherPoint;
    private Rigidbody _rb;


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
    }

    private void OnEnable()
    {
        InputManager.onStartGrapple += StartGrapple;
        InputManager.onEndGrapple += StopGrapple;

        InputManager.onStartHook += StartHooker;
        InputManager.onEndHook += StartHooker;
    }

    private void OnDisable()
    {
        InputManager.onStartGrapple -= StartGrapple;
        InputManager.onEndGrapple -= StopGrapple;

        InputManager.onStartHook -= StartHooker;
        InputManager.onEndHook -= StartHooker;
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
    }

    private void ApplyGrapplePhysics()
    {
        Vector3 directionToGrapple = Vector3.Normalize(_tetherPoint - _player.transform.position);
        float currentDistanceToGrapple = Vector3.Distance(_tetherPoint, _player.transform.position);
        float speedTowardsGrapplePoint = Mathf.Round(Vector3.Dot(_rb.velocity, directionToGrapple) * 100) / 100;

        if (speedTowardsGrapplePoint < 0)
        {
            if (currentDistanceToGrapple > _tetherLength)
            {
                _rb.velocity -= speedTowardsGrapplePoint * directionToGrapple;
                _rb.position = _tetherPoint - directionToGrapple;
            }
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
        _disableGrapple = false;

        if (!_disableGrapple)
        {
            if (Physics.Raycast(_camera.transform.position,_camera.transform.forward, out hit, _maxDistance, _grappleLayer))
            {
               _tethered = true;
               _tetherPoint = hit.point;
               _tetherLength = Vector3.Distance(_tetherPoint, _player.transform.position);
            }
        }
    }

    private void StopGrapple()
    {
        _disableGrapple = true;
        _tethered = false;
        //_lineRenderer.positionCount = 0;
    }

    #endregion


    private void DrawRope()
    {
        if (_tethered)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _tetherPoint);

        }

    }

    #region HOOKSHOT

    private void StartHooker()
    {
        
    }

    private void StopHooker()
    {

    }

    #endregion

}
