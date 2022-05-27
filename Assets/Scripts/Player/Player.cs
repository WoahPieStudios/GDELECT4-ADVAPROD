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

public class Player : MonoBehaviour
{
    public static MovementState movementState = MovementState.GroundMovement;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        
    }

}


