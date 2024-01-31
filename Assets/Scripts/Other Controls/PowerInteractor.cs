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
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
