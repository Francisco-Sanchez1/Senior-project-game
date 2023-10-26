using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Upgrade : MonoBehaviour
{
    private bool hasContact = false;


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if damage has already been applied in this frame
        if (hasContact)
        {
            return;
        }
        PlayerCntrl player = hitInfo.GetComponent<PlayerCntrl>();
        if (player != null)
        {
            hasContact = true;
            player.IncreaseHealth(50);
            Destroy(gameObject);
        }

        
    }

}
