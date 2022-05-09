using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField, Range(1f, 100f)]
    private float _moveSpeed;

    private Rigidbody _rigidBody;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.onStartMovement += MoveDirection;
    }

    private void OnDisable()
    {
        InputManager.onStartMovement -= MoveDirection;
    }

    void FixedUpdate()
    {
        Debug.Log($"{_direction}");
       // _rigidBody.AddForce(_direction * _moveSpeed * Time.deltaTime);
        _rigidBody.MovePosition(transform.position + (transform.right * _direction.x + transform.forward * _direction.z * _moveSpeed * Time.deltaTime));
       // _rigidBody.velocity += _direction * _moveSpeed;
    }

    private void MoveDirection(Vector2 direction)
    {
        _direction = new Vector3(direction.x, transform.position.y, direction.y);
    }
}
