using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool debugSpawnWaves;
    public bool debugSpawnPortal;
    public bool debugSpawnPowerUp;
    public bool debugPowerUpRepel;

    public bool switchLevel {private get; set;}
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
        if(switchLevel)
        {
            SwitchLevels();
        }
    }

    private void EnablePlayer()
    {

    }

    //Extracts the level number from the string to set then load the next level.
    private void SwitchLevels()
    {
        // Stops class from calling this method
        switchLevel = false;

        // Get the name of the currently active scene
        string currentScene = SceneManager.GetActiveScene().name;

        // Extract the level number from the scene name
        int nextLevel = int.Parse(currentScene.Substring(5)) + 1;

        // Check to see it your at the last level
        if (nextLevel <= SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene("Level " + nextLevel.ToString());
        }
        //If at the last level, ends the game.  //*****  More will go here after Prototype  *****//
        else
        {
            gameOver = true;
            Debug.Log("You won");
        }
    }
}