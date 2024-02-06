using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************
 * Component of the ice sphere, 
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/

public class PowerInteractor : MonoBehaviour
{
    [SerializeField] private float pushForce;
    private Rigidbody iceSphereRB;
    private IceSphereController iceSphereController;

    private void Start()
    {
        iceSphereRB = GetComponent<Rigidbody>();
        iceSphereController = GetComponent<IceSphereController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            Rigidbody playerRigidbody = player.GetComponent< Rigidbody>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            Vector3 direction = (player.transform.position - transform.position).normalized;
            if(playerController.hasPowerUp)
            {
                iceSphereRB.AddForce(-direction * playerRigidbody.mass * GameManager.Instance.playerRepelForce, ForceMode.Impulse);
            }
            else
            {
                playerRigidbody.AddForce(direction * playerRigidbody.mass * GameManager.Instance.playerRepelForce, ForceMode.Impulse);
            }
        }
    }
}
