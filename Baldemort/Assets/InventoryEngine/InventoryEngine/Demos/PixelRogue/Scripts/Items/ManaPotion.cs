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

        // Override with the same signature as in the base class
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

            PlayerCntrl player = playerObject.GetComponent<PlayerCntrl>();
            if (player != null)
            {
                // Check if player's mana is already at maximum
                if (player.currentMana >= player.maxMana)
                {
                    Debug.Log("Player " + playerID + " already has maximum mana. Mana Potion not used.");
                    return false;
                }

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