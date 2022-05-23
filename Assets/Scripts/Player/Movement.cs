using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Currently testing out which movement fits best. So far Finished working with the force version and it
/// didn't feel very pleasing to move around. 
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
    
    [SerializeField]
    private float _moveSpeed;

    [Header("Custom Accel Decel")]
    [SerializeField]
    private float _accelerationRate;
    [SerializeField]
    private float _decelerationRate;
    [SerializeField]
    private bool _hasAcceleration;


    private float _currentSpeed;
    private float _speed;
    private bool _canAccelerate = false;

    public float currentSpeed {
        get => _currentSpeed;
        set => _currentSpeed = Mathf.Clamp(value, 0f, _moveSpeed);
    }


    private Rigidbody _rigidBody;
    private Vector3 _direction; // stores input values
    private Vector3 _goingTo; // stores the final values for the direction

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        InputManager.onStartMovement += MoveDirection;
        InputManager.onEndMovement += StopMoving;
    }

    private void OnDisable()
    {
        InputManager.onStartMovement -= MoveDirection;
        InputManager.onEndMovement -= StopMoving;
    }

    private void Update()
    {
        //Debug.Log($"{currentSpeed}");

        if (_canAccelerate)
        {
            currentSpeed += _accelerationRate * Time.deltaTime;
        }else
        {
            
            currentSpeed -= _decelerationRate * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        //references the "forward" of the player where ever the player looks horizontally 
        _goingTo = transform.right * _direction.x + transform.forward * _direction.z;

        _speed = _hasAcceleration ? _currentSpeed : _moveSpeed;
        if (Player.movementState == MovementState.GroundMovement)
            _rigidBody.MovePosition(transform.position + (_goingTo * _speed * Time.deltaTime));


        #region Custom Acceleration Deceleration [ Deprecated ]
        //if (_direction != Vector3.zero)
        //{
        //    _isMoving = true;
        //    if (currentSpeed <= 0.05f)
        //    {
        //        _canAccelerate = true;
        //    }
        //    else if (currentSpeed >= _moveSpeed)
        //    {
        //        _canAccelerate = false;
        //    }
        //}else
        //{
        //    _isMoving = false;
        //}

        //_rigidBody.MovePosition(transform.position + (_goingTo * _moveSpeed * Time.deltaTime));
        #endregion
    }

    //for input direction
    private void MoveDirection(Vector2 direction)
    {
        _direction = new Vector3(direction.x, transform.position.y, direction.y);
        _canAccelerate = true;
    }
    private void StopMoving()
    {
        if (!_hasAcceleration) _direction = new Vector3(0, transform.position.y, 0);
        _canAccelerate = false;
    }

}
