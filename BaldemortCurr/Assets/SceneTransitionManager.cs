using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private Vector3 playerPosition;

    // Function to transition to a new scene while saving the player's position
    public void TransitionToNewScene(string sceneName)
    {
        playerPosition = GameObject.FindWithTag("Player").transform.position; // Assuming the player is tagged as "Player"
        SceneManager.LoadScene(sceneName);
    }

    // Function to return to the previous scene and restore the player's position
    public void ReturnToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Load the previous scene
        GameObject player = GameObject.FindWithTag("Player"); // Find the player object
        if (player != null)
        {
            player.transform.position = playerPosition; // Set the player's position to the saved position
        }
    }
}
