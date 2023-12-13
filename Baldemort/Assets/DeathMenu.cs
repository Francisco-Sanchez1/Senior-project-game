using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public PlayerCntrl playerController;
    private PlayerDataInitializer playerDataInitializer;

    private void Start()
    {
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();
    }

    public void Restart()
    {
        // Respawn the player
        // Get the previous scene's name from PlayerPrefs
        string previousScene = PlayerPrefs.GetString("PreviousScene");

        // Load the previous scene
        SceneManager.LoadScene(previousScene);

        // Set the flag for respawning
        playerDataInitializer.RespawnPlayerData();
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("Title_Screen");
    }
}

