using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBob : MonoBehaviour
{
    /// <summary>
    /// If you want to enable this feature or not
    /// </summary>
    [SerializeField, Tooltip("If you want to enable this feature or not")]
    private bool _enableBobbing = true;

    [SerializeField, Range(0, 0.1f)]
    private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30f)]
    private float _frequency = 10.0f;

    [SerializeField]
    private Transform _camera = null;
    [SerializeField]
    private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private Vector2 _input;
    private Rigidbody _rb;
    private Player _player;

    // Start is called before the first frame update

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
        _startPos = _camera.localPosition;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_enableBobbing) return;

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }
    private void CheckMotion()
    {
        float speed = new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude;

        if (speed < _toggleSpeed) return;
        if (!_player.onGround) return;

        PlayerMotion(FootStepMotion());
    }

    private void PlayerMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private void InputDetection(Vector2 GetInput)
    {
        if (Player.movementState != MovementState.GroundMovement) return;

        _input = GetInput;
        
    }
}
