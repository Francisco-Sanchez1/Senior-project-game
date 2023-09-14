using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealth = 40f;
    public float currentHealth;
    public HealthBar healthBar;
    public const float Between_Hit = 2.0f;


    // Start is called before the first frame update
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void update()
    {
    }

    public void TakeDamage(float damage)
    {
        
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Inv_Frame();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


    IEnumerator Inv_Frame()
    {
        float timeBetweenDamage = 5f;
        yield return new WaitForSeconds(timeBetweenDamage);
    }


}

