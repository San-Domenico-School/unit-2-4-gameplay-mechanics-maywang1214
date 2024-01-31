using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************
 * Component of the spawn manager, 
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/

public class SpawnManager : MonoBehaviour
{
    [Header("Objects to Spawn")]
    [SerializeField] private GameObject iceSphere, portal, powerUp;
    [Header("Wave Fields")]
    [SerializeField] private int initialWave, increaseEachWave, maximumWave;
    [Header("Portal Fields")]
    [SerializeField] private int portalFirstAppearance;
    [SerializeField] private float portalByWaveProbability;
    [SerializeField] private float portalByWaveDuration;
    [Header("PowerUp Fields")]
    [SerializeField] private int powerUpFirstAppearance;
    [SerializeField] private float portalUpByWaveProbability;
    [SerializeField] private float portalUpByWaveDuration;
    [Header("Island")]
    [SerializeField] private GameObject island;
    private Vector3 islandSize;
    private int waveNumber;
    private bool portalActive;
    private bool powerUpActive;

    private void Start()
    {
        islandSize = island.GetComponent<MeshCollider>().bounds.size;
        waveNumber = initialWave;
    }

    private void Update()
    {
        if (FindObjectsOfType<IceSphereController>().Length == 0 && GameObject.Find("Player"))
        {
            SpawnIceWave();
        }
    }

    private void SpawnIceWave()
    {
        int i;
        for(i = 0; i < waveNumber; i += increaseEachWave)
        {
            Instantiate(iceSphere, SetRandomPosition(0f), iceSphere.transform.rotation);
        }
        if(i <= maximumWave)
        {
            waveNumber++;
        }
    }

    private void SetObjectActive(GameObject obj, float byWaveProbability)
    {

    }

    private Vector3 SetRandomPosition(float posY)
    {
        float randomX = Random.Range(-islandSize.x / 2f, islandSize.x / 2f);
        float randomZ = Random.Range(-islandSize.z / 2f, islandSize.z / 2f);
        return new Vector3(randomX, posY, randomZ);
    }

    IEnumerator CountdownTimer(string objectTag)
    {
        yield return null; //******************************** TEMPORARY
    }
}
