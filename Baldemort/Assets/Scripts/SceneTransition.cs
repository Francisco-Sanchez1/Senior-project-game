using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.Tools;

public class DoorTrigger : MonoBehaviour
{
    public int entranceID; // The entrance ID associated with this door trigger
    public string sceneToLoad; // The name of the scene to load when the player walks through the door

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // Perform any actions you want when the player enters the door trigger zone

            // Load the destination scene and pass the entrance ID as a parameter
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            MMGameEvent.Trigger("Save");
            PlayerPrefs.SetInt("EntranceID", entranceID);
        }
    }
}
