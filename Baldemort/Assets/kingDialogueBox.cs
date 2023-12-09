using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingDialogueBox : MonoBehaviour
{
    //when the player collides with the 2d box collider, the king's message will be displayed 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            //find game object with the name "KingMessage" and set it to active
            transform.Find("KingMessage").gameObject.SetActive(true);

        }
    }
}
