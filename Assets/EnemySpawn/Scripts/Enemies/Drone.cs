using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] float movementSpeed = 1f;
    private Rigidbody _rigidBody;
    [Header("Player Reference")]
    [SerializeField] private Transform _playerTransform;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Reset()
    {
        movementSpeed = 1f;
    }

    private void FixedUpdate()
    {
        // Do not do anything if player is not yet found.
        if (_playerTransform == null) return;

        AttackPlayer();
    }

    /// <summary>
    /// Looks for the player and tries to hit it.
    /// </summary>
    private void AttackPlayer()
    {
        Vector3 direction = _playerTransform.position - _rigidBody.transform.position;
        _rigidBody.MovePosition(_rigidBody.transform.position + direction * movementSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
