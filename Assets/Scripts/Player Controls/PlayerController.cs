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
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void FixedUpdate()
    {
        Move();
        if(transform.position.y < -10)
        {
            GameManager.Instance.gameOver = true;
            Debug.Log("You Lost");
        }
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
        if(other.gameObject.CompareTag("Portal"))
        {
            gameObject.layer = LayerMask.NameToLayer("Portal");
        }
        if(other.gameObject.CompareTag("PowerUp"))
        {
            PowerUpController powerUpController = other.GetComponent<PowerUpController>();
            other.gameObject.SetActive(false);
            StartCoroutine(PowerUpCooldown(powerUpController.GetCooldown()));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
            if(transform.position.y <= other.transform.position.y - 1f)
            {
                transform.position = Vector3.up * 25;
                GameManager.Instance.switchLevel = true;
            }
        }
    }

    private IEnumerator PowerUpCooldown(float cooldown)
    {
        hasPowerUp = true;
        powerUpIndicator.intensity = 3.5f;

        yield return new WaitForSeconds(cooldown);

        hasPowerUp = false;
        powerUpIndicator.intensity = 0.0f;
    }
}