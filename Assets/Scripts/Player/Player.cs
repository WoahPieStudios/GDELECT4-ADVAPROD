using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum MovementState
{
    GroundMovement,
    Grappling,
    OnAir
};

/// <summary>
/// Player Component makes it 
/// </summary>
public class Player : MonoBehaviour {
    public static MovementState movementState = MovementState.GroundMovement;

    [SerializeField, Range(1, 3)]
    private float _groundCheckerDistance = 1.25f;

    [SerializeField]
    private float _gravity = 9.8f;

    private bool _onGround;
    public bool onGround
    {
        get => _onGround;
        set => _onGround = value;
    }

    private bool _isWeaponCrateInteracting;

    [SerializeField]
    private float _maxDistanceInteractable;
    [SerializeField]
    private LayerMask _interactableLayer;


    Camera _cam;
    private void Awake()
    {
        Physics.gravity = new Vector3(0, -_gravity, 0);
        
    }

    private void OnEnable()
    {
        InputManager.onPlayerInteraction += CurrentlyInteracting;
        InputManager.onPlayerCancelInteraction += StoppedInteracting;

    }

    private void OnDisable()
    {
        InputManager.onPlayerInteraction -= CurrentlyInteracting;
        InputManager.onPlayerCancelInteraction -= StoppedInteracting;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _cam = Camera.main;
    }

    private void Update()
    {
        #region -= MOVEMENT STATES =-
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hitInfo, _groundCheckerDistance))
        {
            if (hitInfo.collider != null)
            {
                _onGround = true;
            }
        } else
        {
            _onGround = false;
            //transform.position += -transform.up * _gravity *  Time.deltaTime;
        }

        if (_onGround)
        {
            movementState = MovementState.GroundMovement;
        }
        else
        {
            movementState = MovementState.Grappling;
        }

        #endregion

        if (_isWeaponCrateInteracting)
        {
            WeaponCrate interacted = OpenCrate();
            interacted.Holding(Time.deltaTime);

        }



    }

    private WeaponCrate OpenCrate()
    {
        RaycastHit hit;

        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out hit, _maxDistanceInteractable, _interactableLayer))
        {
            WeaponCrate weaponCrate = hit.collider.GetComponent<WeaponCrate>();
            if ( weaponCrate != null)
            {
                return weaponCrate;
            }

        }

        return null;
    }


    private void CurrentlyInteracting()
    {
        _isWeaponCrateInteracting = true;
    }

    private void StoppedInteracting()
    {
        _isWeaponCrateInteracting = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 55);
        Gizmos.DrawRay(transform.position, -transform.up * _groundCheckerDistance);
    }

}


