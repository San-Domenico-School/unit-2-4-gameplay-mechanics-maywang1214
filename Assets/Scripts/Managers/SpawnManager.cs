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
    [SerializeField] private GameObject iceSphere;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject powerUp;

    [Header("Wave Fields")]
    [SerializeField] private int initialWave, increaseEachWave, maximumWave;

    [Header("Portal Fields")]
    [SerializeField] private int portalFirstAppearance;
    [SerializeField] private float portalByWaveProbability;
    [SerializeField] private float portalByWaveDuration;

    [Header("PowerUp Fields")]
    [SerializeField] private int powerUpFirstAppearance;
    [SerializeField] private float powerUpByWaveProbability;
    [SerializeField] private float powerUpByWaveDuration;

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
        if (GameManager.Instance.debugSpawnPortal)
        {
            portalByWaveDuration = 99;
        }
    }

    private void Update()
    {
        if (FindObjectsOfType<IceSphereController>().Length == 0 && GameObject.Find("Player"))
        {
            SpawnIceWave();
        }
        if((waveNumber > portalFirstAppearance || GameManager.Instance.debugSpawnPortal)
            && FindObjectsOfType<ZoomInAnimator>().Length == 0 && !portalActive)
        {
            SetObjectActive(portal, portalByWaveProbability);
        }
        if((waveNumber > powerUpFirstAppearance || GameManager.Instance.debugSpawnPowerUp) && !powerUpActive)
        {
            SetObjectActive(powerUp, powerUpByWaveProbability);
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
        if (Random.value < waveNumber * byWaveProbability * Time.deltaTime || GameManager.Instance.debugSpawnPortal)
        {
            obj.transform.position = SetRandomPosition(obj.transform.position.y);
            StartCoroutine(CountdownTimer(obj.tag));
        }
    }

    private Vector3 SetRandomPosition(float posY)
    {
        float randomX = Random.Range(-islandSize.x / 2.75f, islandSize.x / 2.75f);
        float randomZ = Random.Range(-islandSize.z / 2.75f, islandSize.z / 2.75f);
        return new Vector3(randomX, posY, randomZ);
    }

    IEnumerator CountdownTimer(string objectTag)
    {
        float byWaveDuration = 0;

        Debug.Log(objectTag);
        switch (objectTag)
        {
            case "Portal":
                portal.SetActive(true);
                portalActive = true;
                byWaveDuration = portalByWaveDuration;
                break;
            case "PowerUp":
                powerUp.SetActive(true);
                powerUpActive = true;
                byWaveDuration = powerUpByWaveDuration;
                break;
        }

        yield return new WaitForSeconds(50);// waveNumber * byWaveDuration);

        switch (objectTag)
        {
            case "Portal":
                portal.SetActive(false);
                portalActive = false;
                break;
            case "PowerUp":
                powerUp.SetActive(false);
                powerUpActive = false;
                break;
        }
    }   
}
