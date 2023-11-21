using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private string spawnPointName = "DefaultSpawnPoint"; // A default spawn point

    private void Awake()
    {
        // Retrieve the spawn point name from PlayerPrefs
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            spawnPointName = PlayerPrefs.GetString("SpawnPoint");
        }

        // Load the player's position based on the spawn point
        switch (spawnPointName)
        {
            case "TownSpawnPoint":
                // Set the player's position for the town_map scene
                SceneManager.LoadScene("town_map");
                // Place the player at the TownSpawnPoint position
                // You can access and set the player's position as needed.
                break;

            case "CastleSpawnPoint":
                // Set the player's position for the castle scene
                SceneManager.LoadScene("castle");
                // Place the player at the CastleSpawnPoint position
                // You can access and set the player's position as needed.
                break;

            // Add more cases for other spawn points

            default:
                // Handle other cases or set a default spawn point
                break;
        }

        // Optionally, clear the PlayerPrefs key
        PlayerPrefs.DeleteKey("SpawnPoint");

        // Make this GameObject persist across scenes
        DontDestroyOnLoad(gameObject);
    }

    // Function to set the spawn point (e.g., called when transitioning between scenes)
    public void SetSpawnPoint(string newSpawnPoint)
    {
        PlayerPrefs.SetString("SpawnPoint", newSpawnPoint);
    }
}
