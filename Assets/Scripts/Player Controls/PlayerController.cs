using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*******************************************************
 * Component of the player, takes in user
 * imput to move and turn the player
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private SphereCollider playerCollider;
    private Light powerUpIndicator;
    private PlayerInputActions inputAction;
    private Transform focalpoint;
    private float forwardOrBackward;
    private float moveForce;
    private float moveDirection;
    public bool hasPowerUp { get; private set; }

    private void Awake()
    {
        inputAction = new PlayerInputActions();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerRB = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        powerUpIndicator = GetComponent<Light>();
        playerCollider.material.bounciness = 0.4f;
        powerUpIndicator.intensity = 0;
    }

    private void OnEnable()
    {
        inputAction.Enable();
        inputAction.Player.Movement.performed += OnMovementPerformed;
        inputAction.Player.Movement.canceled += OnMovementCanceled;
        if(GameManager.Instance.debugPowerUpRepel)
        {
            hasPowerUp = true;
        }
    }

    private void OnDisable()
    {
        inputAction.Disable();
        inputAction.Player.Movement.performed -= OnMovementPerformed;
        inputAction.Player.Movement.canceled -= OnMovementCanceled;
    }

    private void Update()
    {

    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        forwardOrBackward = value.ReadValue<Vector2>().y;
    }

    private void OnMovementCanceled(InputAction.CallbackContext value)
    {
        forwardOrBackward = 0;
    }

    private void AssignLevelValues()
    {
        transform.position = GameManager.Instance.playerStartPos;
        transform.localScale = GameManager.Instance.playerScale;
        playerRB.mass = GameManager.Instance.playerMass;
        playerRB.drag = GameManager.Instance.playerDrag;
        moveForce = GameManager.Instance.playerMoveForce;
        focalpoint = GameObject.Find("Focal Point").transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(focalpoint != null)
        {
            playerRB.AddForce(focalpoint.forward * moveForce * forwardOrBackward);
        }
    }
                                             
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Startup")
        {
            collision.gameObject.tag = "Ground";
            playerCollider.material.bounciness = GameManager.Instance.playerBounce;
            AssignLevelValues();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }

    private IEnumerator PowerUpCooldown(float cooldown)
    {
        yield return null;  // replace with correct code later
    }
}
