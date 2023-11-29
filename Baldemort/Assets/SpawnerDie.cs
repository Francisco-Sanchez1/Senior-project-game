using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDie : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    public bool poisoned = false;
    public float poisonTimerfull = 1f;
    public float PoisonTick = 10f;
    public float invincibilityDuration = 0.5f;
    public SpriteRenderer mySprite;

    private PlayerDataInitializer playerDataInitializer;

    public bool onFire = false;
    public float FireTimerFull = 1f;
    public float FireTick = 5f;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();


    }


    //DAMAGE VARIANTS
    public void fireHurt(float fire)
    {
        onFire = true;
        StartCoroutine(FireDamage(fire));
    }

    IEnumerator FireDamage(float fire)
    {
        int temp = 0;
        while (temp <= FireTick)
        {
            currentHealth -= fire;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            yield return new WaitForSeconds(FireTimerFull);

            temp++;
        }
        onFire = false;
    }

    public void PoisonHurt(float poison)
    {
        poisoned = true;
        StartCoroutine(PoisonDamage(poison));
    }

    IEnumerator PoisonDamage(float poison)
    {
        int temp = 0;
        while (temp <= PoisonTick)
        {
            currentHealth -= poison;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
            yield return new WaitForSeconds(poisonTimerfull);

            temp++;
        }
        poisoned = false;
        mySprite.color = Color.white; // Reset color after poison effect
    }

    
    public void TakeDamage(float damage)
    {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //DEATH

    void Die()
    {
        Debug.Log("My name is: " + gameObject.name);
        PlayerPrefs.SetInt(gameObject.name + "_IsDead", 1); // Save the state as dead
        PlayerPrefs.Save();
        gameObject.SetActive(false); 
    }
}
