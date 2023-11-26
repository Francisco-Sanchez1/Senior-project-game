using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoin_Pickup : MonoBehaviour
{
    private bool hasContact = false;
    public int minCol = 6;
    public int maxCol = 20;

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
            int randomAmount = Random.Range(minCol, maxCol);
            hasContact = true;
            Debug.Log("Random Coins Amount: " + randomAmount);
            player.Coin_Pick(randomAmount);
            Destroy(gameObject);
        }


    }
}
