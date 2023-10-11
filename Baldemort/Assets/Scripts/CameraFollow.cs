using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // The player's transform
    public float smoothSpeed = 0.125f; // The speed at which the camera follows the player. Adjust to your liking.
    public Vector3 offset; // Any offset if you want the camera to be, for example, slightly above the player.

    private void FixedUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}