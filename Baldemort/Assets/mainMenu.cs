using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{


    public PlayerDataInitializer playerDataInitializer;

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Play()
    {
        playerDataInitializer.ResetSavedData();
        playerDataInitializer.ResetBossStatus();
        PlayerPrefs.SetInt("EntranceID", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
        
    }

}