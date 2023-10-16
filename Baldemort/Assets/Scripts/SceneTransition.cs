using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene to load when the player walks through the door

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // Perform any actions you want when the player enters the door trigger zone
            // For example, load a new scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
