using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float smoothSpeed = 0.125f;

    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(xOffset, yOffset, transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position,targetPosition,smoothSpeed + Time.deltaTime);
        transform.position = smoothPosition;
    }
}
