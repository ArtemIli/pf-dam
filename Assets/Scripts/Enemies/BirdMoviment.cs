using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMoviment : MonoBehaviour
{
    [SerializeField]
    private float speedX = 1.0f;

    void Update()
    {
        transform.Translate(speedX * Time.deltaTime, 0, 0);
    }
}
