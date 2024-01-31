using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************
 * Component of the ice sphere, 
 * 
 * Yuqin Wang
 * January 16, 2024 version 1.0
 * ****************************************************/

public class IceSphereController : MonoBehaviour
{
    [SerializeField] private float startDelay, reductionEachRepeat, minimumVolume;
    private Rigidbody iceRB;
    private ParticleSystem iceVFX;
    private float pushForce;

    private void Start()
    {
        if (GameManager.Instance.debugSpawnWaves)
        {
            reductionEachRepeat = .5f;
        }
        iceRB = GetComponent<Rigidbody>();
        iceVFX = GetComponent<ParticleSystem>();
        RandomizeSizeAndMass();
        InvokeRepeating("Melt", startDelay, 0.4f);
    }

    private void RandomizeSizeAndMass()
    {
        float randomScale = Random.Range(0.5f, 1.0f);
        float randomMass = Random.Range(0.5f, 1.0f);
        transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        iceRB.mass = randomMass;
    }

    private void Dissolution()
    {
        float volume = 4f / 3f * Mathf.PI * Mathf.Pow(transform.localScale.x, 2);
        if (volume < 0.8 && FindObjectsOfType<IceSphereController>().Length > 1)
        {
            iceVFX.Stop();
        }
        if (volume < minimumVolume)
        { 
            Destroy(gameObject);
        }
    }

    private void Melt()
    {
        transform.localScale *= reductionEachRepeat;
        iceRB.mass *= reductionEachRepeat;
        Dissolution();
    }
}
