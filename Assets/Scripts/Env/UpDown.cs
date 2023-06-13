using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [SerializeField]
    private float moveDistance = 10f;
    [SerializeField]
    private float moveSpeed = 1f;
    [SerializeField]
    private float waitTime = 10f;

    private Vector3 initialPosition;
    private float timer;
    private Boolean movingUp;

    void Start()
    {
        initialPosition = transform.position;
        timer = 0f;
        movingUp = true;
    }

    void Update()
    {

        timer += Time.deltaTime;

        if(timer >= waitTime)
        {
            timer = 0f;
            movingUp = !movingUp;
        }

        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition + Vector3.up * moveDistance, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        }

    }
}
