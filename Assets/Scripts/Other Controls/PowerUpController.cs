using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************
 * Component of the power up, 
 * 
 * Yuqin Wang
 * Feburary 28, 2024 version 1.0
 * ****************************************************/

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private float cooldown, rotationSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}