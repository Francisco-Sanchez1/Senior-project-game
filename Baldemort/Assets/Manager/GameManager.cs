using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints; // List of spawn points with entrance IDs
    public Transform player; // Reference to the player GameObject

    // Define a default entrance ID for resetting the spawn point
    private int defaultEntranceID = 1;

    void Start()
    {
        int entranceID = PlayerPrefs.GetInt("EntranceID", defaultEntranceID); // Get the entrance ID from PlayerPrefs (default to 1)

        // Find the corresponding spawn point for the entrance ID
        SpawnPoint spawnPoint = spawnPoints.Find(sp => sp.entranceID == entranceID);

        if (spawnPoint != null)
        {
            // Move the player to the chosen spawn point's position
            player.position = spawnPoint.transform.position;
        }
        else
        {
            // Handle the case where the entrance ID doesn't match any spawn point
            Debug.LogWarning("Entrance ID not found. Player not moved.");

            // Reset the entrance ID to the default value
            PlayerPrefs.SetInt("EntranceID", defaultEntranceID);
            PlayerPrefs.Save();
        }
    }

    // Add a method to reset the entrance ID to the default value
    public void ResetSpawnPoint()
    {
        PlayerPrefs.SetInt("EntranceID", defaultEntranceID);
        PlayerPrefs.Save();
    }
}
