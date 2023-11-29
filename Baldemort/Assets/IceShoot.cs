using UnityEngine;

public class IceShoot : BaseProjectile
{
    // Flag to prevent multiple damage applications in a single frame
    private bool hasDealtDamage = false;
    public float manaCost = 20f;

    // Reference to the PlayerDamageStats script on the player
    private DamageUpgradeIce damageUpgradeice;

    private void Start()
    {
        damageUpgradeice = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeIce>();
    }

    public void SetManaCost(float cost)
    {
        manaCost = cost;
    }



    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if damage has already been applied in this frame
        if (hasDealtDamage)
        {
            return;
        }
        SpawnerDie spawner = hitInfo.GetComponent<SpawnerDie>();
        ChestDie Chest = hitInfo.GetComponent<ChestDie>();
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            int randomDamage = Random.Range(damageUpgradeice.currentMinDamage, damageUpgradeice.currentMaxDamage);
            Debug.Log("Random damage applied Ice: " + randomDamage);
            enemy.TakeDamage(randomDamage);
            hitInfo.GetComponent<Enemy>().currentState = EnemyState.Freeze;

            // Set the flag to true to prevent further damage applications in this frame
            hasDealtDamage = true;
        }
        if (spawner != null)
        {
            int randomDamage = Random.Range(damageUpgradeice.currentMinDamage, damageUpgradeice.currentMaxDamage);
            spawner.TakeDamage(randomDamage);
        }
        if (Chest != null)
        {
            int randomDamage = Random.Range(damageUpgradeice.currentMinDamage, damageUpgradeice.currentMaxDamage);
            Chest.TakeDamage(randomDamage);
        }
        if (hitInfo.gameObject.CompareTag("Player") || (hitInfo.gameObject.CompareTag("Attack") || hitInfo.gameObject.CompareTag("Collectable")))
        {
            return;
        }

        Destroy(gameObject);
    }
}


