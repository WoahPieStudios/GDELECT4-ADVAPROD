using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// MUST be attached to a separate empty object and not to a player
/// </summary>
public class Grapple : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Vector3 _grapplePoint;
    private Camera _camera;
    private SpringJoint _springJoint;

    [SerializeField]
    private LayerMask _grappleLayer;
    [SerializeField]
    private GameObject _player;
    [SerializeField, Tooltip("max distance that the player can grapple"), Range(10f, 100f)]
    private float _maxDistance;

    [Space]
    [Header("Spring Values")]
    [SerializeField, Range (.1f, 15f)]
    private float _spring = 4.5f;
    [SerializeField, Range(.1f, 15f)]
    private float _damper = 7f;
    [SerializeField, Range(.1f, 15f)]
    private float _massScale = 4.5f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
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
    }

    private void OnDisable()
    {
        InputManager.onStartGrapple -= StartGrapple;
        InputManager.onEndGrapple -= StopGrapple;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        DrawRope();
    }
    private void StartGrapple()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position,_camera.transform.forward, out hit, _maxDistance, _grappleLayer))
        {
            _grapplePoint = hit.point;
            _springJoint = _player.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = _grapplePoint;

            float distanceFromPoint = Vector3.Distance(_player.transform.position, _grapplePoint);

            _springJoint.maxDistance = distanceFromPoint * 0.8f;
            _springJoint.minDistance = distanceFromPoint * 0.25f;



            _springJoint.spring = _spring;
            _springJoint.damper = _damper;
            _springJoint.massScale = _massScale;

            _lineRenderer.positionCount = 2;
        }
    }

    private void StopGrapple()
    {
        _lineRenderer.positionCount = 0;
        Destroy(_springJoint);
    }

    private void DrawRope()
    {
        if (!_springJoint) return;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, _grapplePoint);
    }

}
