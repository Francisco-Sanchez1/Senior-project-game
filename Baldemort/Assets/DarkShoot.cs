using UnityEngine;

public class DarkShoot : BaseProjectile
{
    // Flag to prevent multiple damage applications in a single frame
    private bool hasDealtDamage = false;
    public float manaCost = 10f;

    // Reference to the PlayerDamageStats script on the player
    private DamageUpgradeDark damageUpgradedark;

    private void Start()
    {
        damageUpgradedark = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgradeDark>();
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
            int randomDamage = Random.Range(damageUpgradedark.currentMinDamage, damageUpgradedark.currentMaxDamage);
            Debug.Log("Random damage applied Dark: " + randomDamage);
            enemy.TakeDamage(randomDamage);

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

