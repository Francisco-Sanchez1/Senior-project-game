using UnityEngine;

public class DamageUpgradeDark : MonoBehaviour
{
    //public int baseMinDamage = 6;
    //public int baseMaxDamage = 10;
    public int currentMinDamage;
    public int currentMaxDamage;
    public bool damageUpgraded = false;

    public int upgradeCost = 20; // Reference to the Shooting script

    private PlayerDataInitializer playerDataInitializer;

    private void Start()
    {
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();

        // Initialize damage values with base values
        currentMinDamage = playerDataInitializer.minDark;
        currentMaxDamage = playerDataInitializer.maxDark;
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
            playerDataInitializer.minDark += 5;
            currentMaxDamage += 5;
            playerDataInitializer.maxDark += 5;
            damageUpgraded = true;
            playerDataInitializer.dark1Up = true;
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
