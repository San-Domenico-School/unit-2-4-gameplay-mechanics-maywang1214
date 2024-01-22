using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************
 * Component of the game manager, 
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Player Flelds")]
    public Vector3 playerScale, playerStartPos;
    public float playerMass, playerDrag, playerMoveForce,
        playerRepelForce, playerBounce;
    [Header("Scene Flelds")]
    public GameObject[] waypoints;
    [Header("Debug Flelds")]
    public bool debugSpawnWaves, debugSpawnPortal, debugSpawnPowerUp,
        debugPowerUpRepel;
    public bool switchLevels {private get; set;}
    public bool gameOver {private get; set;}
    public bool playerHasPowerUp {get; set;}

    private void Awake()
    {
        // Awake is called before any Start methods are called
        //This is a common approach to handling a class with a reference to itself.
        //If instance variable doesn't exist, assign this object to it
        if (Instance == null)
        {
            Instance = this;
        }
        //Otherwise, if the instance variable does exist, but it isn't this object, destroy this object.
        //This is useful so that we cannot have more than one GameManager object in a scene at a time.
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void EnablePlayer()
    {

    }

    private void SwitchLevels()
    {

    }
}