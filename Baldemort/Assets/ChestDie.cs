using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDie : MonoBehaviour
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

    public GameObject itemToDropPrefab1;
    public GameObject itemToDropPrefab2;
    public GameObject itemToDropPrefab3;
    public GameObject DeadChestPrefab;

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
        GameObject newItem = null;
        GameObject ChestItem = null;
        float randomChance = Random.Range(0f, 1f);

        if (randomChance < 0.5f)
        {
            Debug.Log("Item drop successful, dropping item 1");
            newItem = Instantiate(itemToDropPrefab1, transform.position, Quaternion.identity);
        }
        else if (randomChance < 0.8f)
        {
            Debug.Log("Item drop successful, dropping item 2");
            newItem = Instantiate(itemToDropPrefab2, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Item drop successful, dropping item 3");
            newItem = Instantiate(itemToDropPrefab3, transform.position, Quaternion.identity);
        }

        if (newItem != null)
        {
            // Set the sorting layer and order in layer
            SpriteRenderer itemSpriteRenderer = newItem.GetComponent<SpriteRenderer>();
            if (itemSpriteRenderer != null)
            {
                itemSpriteRenderer.sortingLayerName = "SpawnedObjects"; // Set to your desired layer
                itemSpriteRenderer.sortingOrder = 10; // Set to your desired order in layer
            }
        }

        Debug.Log("My name is: " + gameObject.name);
        PlayerPrefs.SetInt(gameObject.name + "_IsDead", 1); // Save the state as dead
        PlayerPrefs.Save();
        gameObject.SetActive(false);
        ChestItem = Instantiate(DeadChestPrefab, transform.position, Quaternion.identity);
        SpriteRenderer ChestSpriteRenderer = ChestItem.GetComponent<SpriteRenderer>();
        ChestSpriteRenderer.sortingOrder = 1; 
    }


}


