using UnityEngine;

public class FireShoot : BaseProjectile
{
    // Flag to prevent multiple damage applications in a single frame
    private bool hasDealtDamage = false;
    public float manaCost = 20f;
    public GameObject flamesPrefab;
    // Reference to the PlayerDamageStats script on the player
    private DamageUpgradeFire damageUpgradefire;

    private void Start()
    {
        damageUpgradefire = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeFire>();
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

        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            int randomDamage = Random.Range(damageUpgradefire.currentMinDamage, damageUpgradefire.currentMaxDamage);
            Debug.Log("Random damage applied Fire: " + randomDamage);
            enemy.TakeDamage(randomDamage);

            Instantiate(flamesPrefab, hitInfo.transform.position, Quaternion.identity);
            // Set the flag to true to prevent further damage applications in this frame
            hasDealtDamage = true;
        }

        if (hitInfo.gameObject.CompareTag("Player") || (hitInfo.gameObject.CompareTag("Attack") || hitInfo.gameObject.CompareTag("Collectable")))
        {
            return;
        }

        Destroy(gameObject);
    }
}

