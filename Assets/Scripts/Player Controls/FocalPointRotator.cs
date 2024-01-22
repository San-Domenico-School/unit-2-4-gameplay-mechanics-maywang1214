using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*******************************************************
 * Component of the focal point, 
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/

public class FocalPointRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    private PlayerInputActions inputAction;
    private float moveDirection;

    private void Awake()
    {
        inputAction = new PlayerInputActions();
    }

    // Rotate the foal points which the camera is attached to
    private void Update()
    {
        transform.Rotate(Vector3.up, moveDirection * rotationSpeed * Time.deltaTime);
    }

    // Add OnMovement events to inputAction's Player's movement
    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Movement.performed += OnMovementPerformed;
        inputAction.Player.Movement.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        inputAction.Disable();
        inputAction.Player.Movement.performed -= OnMovementPerformed;
        inputAction.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveDirection = value.ReadValue<Vector2>().x;
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        moveDirection = 0;
    }
}
