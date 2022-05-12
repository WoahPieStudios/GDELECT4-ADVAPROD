using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Currently testing out which movement fits best. So far Finished working with the force version and it
/// didn't feel very pleasing to move around. 
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
    
    [Header("Force")]
    [SerializeField, Range(1f, 100f)]
    private float _forceSpeed; // Amount of force that will be given to the player
    [SerializeField, Tooltip("The rate on how frequent adding the force")]
    private float _rateOfPulseSpeed; 
    private float _lastPulse;
    [SerializeField, Tooltip("only ForceModes working: Impulse, ChangeVelocity")] 
    private ForceMode _forceMode;


    [Header("Custom Accel Decel")]
    [SerializeField, Range(1f, 5f)]
    private float _moveSpeed;
    [SerializeField]
    private float _accelerationRate;
    [SerializeField]
    private float _decelerationRate;
    private float _currentSpeed;
    private bool _canAccelerate = false;
    private bool _isMoving = false;

    public float currentSpeed {
        get => _currentSpeed;
        set => _currentSpeed = Mathf.Clamp(value, 0f, _moveSpeed);
    }


    private Rigidbody _rigidBody;
    private Vector3 _direction;
    private Vector3 _goingTo;

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
    }

    private void OnDisable()
    {
        InputManager.onStartMovement -= MoveDirection;
    }

    private void Update()
    {
        Debug.Log($"{currentSpeed}");
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

        #region Custom Acceleration Deceleration
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

        _rigidBody.MovePosition(transform.position + (_goingTo * _moveSpeed * Time.deltaTime));
        #endregion
    }

    private void MoveDirection(Vector2 direction)
    {
        _direction = new Vector3(direction.x, transform.position.y, direction.y);
    }
}
