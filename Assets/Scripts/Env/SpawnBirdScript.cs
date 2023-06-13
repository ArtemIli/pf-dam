using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBirdScript : MonoBehaviour
{
    [SerializeField] private GameObject bird;
    [SerializeField] private float spawnInterval = 2.0f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 0f, spawnInterval);
    }

    void SpawnObject()
    {
        Instantiate(bird, transform.position, Quaternion.identity);
    }
}
