using System.Collections;
using System.Collections.Generic;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField, Range (0.1f, 10f)]
    private float _mouseSensitivityX = 1f;
    [SerializeField, Range (0.1f, 10f)]
    private float _mouseSensitivityY = 1f;
    [SerializeField]
    private Transform _playerTransform;

    private float _mouseX, _mouseY;
    private float _xClamp = 85f;
    private float _xRotation = 0f;

    [SerializeField] private PauseEventChannel pauseEvent;
    private bool canLook;

    public void MouseInput(Vector2 mouseInput)
    {
        _mouseX = mouseInput.x * _mouseSensitivityX;
        _mouseY = mouseInput.y * _mouseSensitivityY;
    }

    private void OnEnable()
    {
        InputManager.onMouseLook += MouseInput;
        canLook = true;
        pauseEvent.AddPauseListener(() => { canLook = false;});
        pauseEvent.AddResumeListener(() => { canLook = true;});
    }

    private void OnDisable()
    {
        InputManager.onMouseLook -= MouseInput;
        pauseEvent.RemovePauseListener(() => { canLook = false;});
        pauseEvent.RemoveResumeListener(() => { canLook = true;});
    }

    private void Update()
    {
        if (!canLook) return;
        _playerTransform.Rotate(Vector3.up, _mouseX * Time.deltaTime);

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = _xRotation;
        transform.eulerAngles = targetRotation;
    }
}
