using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEggs : MonoBehaviour
{
    [SerializeField] private GameObject eggs;
    [SerializeField] private float timeInterval = 5.0f;

    private void Start()
    {
        InvokeRepeating("SpawnObjects", 0, timeInterval);
    }

    private void SpawnObjects()
    {
        Instantiate(eggs, transform.position, Quaternion.identity);
    }

}
