using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "ManaPotion", menuName = "MoreMountains/InventoryEngine/ManaPotion", order = 1)]
    [Serializable]
    public class ManaPotion : InventoryItem
    {
        [Header("Mana Bonus")]
        public int ManaBonus;

        public override bool Use(string playerID)
        {
            base.Use(playerID);
            Debug.Log("Attempting to use Mana Potion for: " + playerID);

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogError("No GameObject found with the 'Player' tag.");
                return false;
            }

            // Optionally, if you want to check if the playerID matches the found player's ID or name
            if (playerObject.name != playerID)
            {
                Debug.LogError("The player ID does not match the tagged player object.");
                return false;
            }

            PlayerCntrl player = playerObject.GetComponent<PlayerCntrl>();
            if (player != null)
            {
                player.RegainMana(ManaBonus);
                Debug.Log("Increased character " + playerID + "'s mana by " + ManaBonus);
                return true;
            }
            else
            {
                Debug.LogError("PlayerCntrl component not found on GameObject with 'Player' tag.");
                return false;
            }
        }
    }
}