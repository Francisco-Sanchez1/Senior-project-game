using UnityEngine;

public class DamageUpgradeZap : MonoBehaviour
{
    //public int baseMinDamage = 8;
    //public int baseMaxDamage = 14;
    public int currentMinDamage;
    public int currentMaxDamage;
    public bool damageUpgraded;

    public int upgradeCost = 20; // Reference to the Shooting script

    private PlayerDataInitializer playerDataInitializer;


    private void Start()
    {
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();
        // Initialize damage values with base values
        currentMinDamage = playerDataInitializer.minZap;
        currentMaxDamage = playerDataInitializer.maxZap;
        damageUpgraded = false;
    }

    public void UpgradeDamage(int upgradeCost)
    {
        if (PlayerHasEnoughCoins(upgradeCost))
        {
            // Deduct the upgrade cost from the player's coins.
            DeductCoins(upgradeCost);

            // Upgrade the damage in the Shooting script.
            // Increase the damage when upgrading
            currentMinDamage += 5;
            playerDataInitializer.minZap += 5;
            currentMaxDamage += 5;
            playerDataInitializer.maxZap += 5;
            damageUpgraded = true;
            playerDataInitializer.zap1Up = true;


            Debug.Log("Damage upgraded!");
        }
        else
        {
            Debug.Log("Not enough coins for the upgrade!");
        }
    }

    public bool PlayerHasEnoughCoins(int cost)
    {
        // Check if the player has enough coins to cover the cost.
        PlayerCntrl player = GetComponent<PlayerCntrl>();
        return player != null && player.coins >= cost;
    }

    public void ResetDamage()
    {
        // Reset damage values to their base values
        damageUpgraded = false;
    }


    public void DeductCoins(int amount)
    {
        // Deduct coins from the player's total.
        PlayerCntrl player = GetComponent<PlayerCntrl>();
        if (player != null)
        {
            player.SpendCoin(amount);
        }
    }



}
