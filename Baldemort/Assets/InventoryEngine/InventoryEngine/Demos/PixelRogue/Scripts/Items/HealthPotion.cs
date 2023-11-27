using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "MoreMountains/InventoryEngine/HealthPotion", order = 1)]
    [Serializable]
    public class HealthPotion : InventoryItem
    {
        [Header("Health Bonus")]
        public int HealthBonus;

        // Override with the same signature as in the base class
        public override bool Use(string playerID)
        {
            base.Use(playerID);
            Debug.Log("Attempting to use Health Potion for: " + playerID);

            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject == null)
            {
                Debug.LogError("No GameObject found with the 'Player' tag.");
                return false;
            }

            PlayerCntrl player = playerObject.GetComponent<PlayerCntrl>();
            if (player != null)
            {
                // Check if player's health is already at maximum
                if (player.currentHealth >= player.maxHealth)
                {
                    Debug.Log("Player " + playerID + " already has maximum health. Health Potion not used.");
                    return false;
                }

                player.RegainHealth(HealthBonus);
                Debug.Log("Increased character " + playerID + "'s halth by " + HealthBonus);
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