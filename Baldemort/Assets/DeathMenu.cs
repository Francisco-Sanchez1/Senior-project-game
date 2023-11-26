using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour
{
    public PlayerCntrl playerController;

    public void Restart()
    {
        SceneManager.LoadScene("town_map");
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("Title_Screen");

    }



}
