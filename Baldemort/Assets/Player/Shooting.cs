using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Flag to prevent multiple damage applications in a single frame
    private bool hasDealtDamage = false;
    public float manaCostIce = 5f;

    // Reference to the PlayerDamageStats script on the player
    private DamageUpgrade damageUpgrade;
    private attack Attack;
    private void Start()
    {
        damageUpgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<DamageUpgrade>();
    }

    public void SetManaCost(float cost)
    {
        manaCostIce = cost;
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
            int randomDamage = Random.Range(damageUpgrade.currentMinDamage, damageUpgrade.currentMaxDamage);
            Debug.Log("Random damage applied: " + randomDamage);
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
