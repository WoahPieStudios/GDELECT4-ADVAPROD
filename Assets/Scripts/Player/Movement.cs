using UnityEngine;

/// <summary>
/// Currently testing out which movement fits best. So far Finished working with the force version and it
/// didn't feel very pleasing to move around. 
/// </summary>

[DefaultExecutionOrder(1000)]
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
    
    [SerializeField]
    private float _moveSpeed;

    private float _accelerationRate;
    private float _decelerationRate;
    private bool _canAccelerate = false;

    private Player _player;
    
    /// <summary>
    /// stores current Speed of the player
    /// </summary>
    private float _currentSpeed;
    public float currentSpeed {
        get => _currentSpeed;
        set => _currentSpeed = Mathf.Clamp(value, 0f, _moveSpeed);
    }


    private Rigidbody _rigidBody;

    /// <summary>
    /// stores input values
    /// </summary>
    private Vector3 _inputDirection; // stores input values for movement

    /// <summary>
    /// Stores direction of player going to
    /// </summary>
    private Vector3 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        if (_moveSpeed < Physics.gravity.y) _moveSpeed = -Physics.gravity.y; 
        _accelerationRate = _moveSpeed * _moveSpeed / 10;
        _decelerationRate = _moveSpeed * 10f + 20;
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
        //Debug.LogError($"{_canAccelerate}");

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
        _direction = transform.right * _inputDirection.x + transform.forward * _inputDirection.z;

        if (_player.onGround)
        {
            _rigidBody.velocity += _direction * _currentSpeed * Time.deltaTime; 
   
        }

        ///<summary>
        /// Player's movement speed will be reduced while on Air
        ///</summary>
        if (Player.movementState == MovementState.OnAir)
        {
            float speed_On_Air = _currentSpeed / 2 + 25;
            if (speed_On_Air < Physics.gravity.y)
            {
                speed_On_Air = Physics.gravity.y + 10f;
            }
            _rigidBody.velocity += _direction * speed_On_Air * Time.deltaTime;
        }
    }

    //for input direction
    private void MoveDirection(Vector2 direction)
    {
        _inputDirection = new Vector3(direction.x, transform.position.y, direction.y);
        _canAccelerate = true;
    }
    private void StopMoving()
    {
        _canAccelerate = false;
    }



}
