using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Check if the ESC key is pressed or I key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.I))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause the game
            Time.timeScale = 0;
            //log game is pause
            Debug.Log("Game is paused");
        }
        else
        {
            // Resume the game
            Time.timeScale = 1;
            //log game is resume
            Debug.Log("Game is resumed");
        }
    }
}