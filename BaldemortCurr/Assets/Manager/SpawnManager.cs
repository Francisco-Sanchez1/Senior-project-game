using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Public variables for spawn points
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;

    // Public variable for entrance ID
    public int entranceID;

    void Start()
    {
        // Check the entrance ID and set the player's position accordingly
        Transform player = GameObject.FindWithTag("Player").transform; // Assuming the player is tagged as "Player"

        switch (entranceID)
        {
            case 1:
                player.position = spawnPoint1.position;
                break;
            case 2:
                player.position = spawnPoint2.position;
                break;
            case 3:
                player.position = spawnPoint3.position;
                break;
            default:
                Debug.LogWarning("Invalid entrance ID: " + entranceID);
                break;
        }
    }
}
