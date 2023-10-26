using UnityEngine;

public class PlayerDamageStats : MonoBehaviour
{
    public int baseMinDamage = 8;
    public int baseMaxDamage = 14;
    public int currentMinDamage;
    public int currentMaxDamage;
    public bool damageUpgraded = false;

    private void Start()
    {
        // Initialize damage values with base values
        currentMinDamage = baseMinDamage;
        currentMaxDamage = baseMaxDamage;
    }

    public void UpgradeDamage()
    {
        // Increase the damage when upgrading
        currentMinDamage += 5;
        currentMaxDamage += 5;
        damageUpgraded = true;
    }

    public void ResetDamage()
    {
        // Reset damage values to their base values
        currentMinDamage = baseMinDamage;
        currentMaxDamage = baseMaxDamage;
        damageUpgraded = false;
    }
}
