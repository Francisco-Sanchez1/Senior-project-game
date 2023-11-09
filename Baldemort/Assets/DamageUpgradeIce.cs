using UnityEngine;

public class DamageUpgradeIce : MonoBehaviour
{
    //public int baseMinDamage = 2;
    //public int baseMaxDamage = 8;
    public int currentMinDamage;
    public int currentMaxDamage;
    public bool damageUpgraded = false;

    public int upgradeCost = 20; // Reference to the Shooting script

    private PlayerDataInitializer playerDataInitializer;

    private void Start()
    {
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();

        // Initialize damage values with base values
        currentMinDamage = playerDataInitializer.minIce;
        currentMaxDamage = playerDataInitializer.maxIce;
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
            playerDataInitializer.minIce += 5;
            currentMaxDamage += 5;
            playerDataInitializer.maxIce += 5;
            damageUpgraded = true;
            playerDataInitializer.ice1Up = true;

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
        //currentMinDamage = baseMinDamage;
        //currentMaxDamage = baseMaxDamage;
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
