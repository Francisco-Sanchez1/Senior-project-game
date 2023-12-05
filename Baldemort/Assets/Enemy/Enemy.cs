using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    summon,
    attackRange,
    Freeze,
    attackThrust,
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;
    public int baseAttack;



    public GameObject nextSceneObject;
    public GameObject caveSceneObject;
    public GameObject itemToDropPrefab;
    public GameObject BlockadeSceneObject;

    public GameObject itemToDropPrefab1;
    public GameObject itemToDropPrefab2;
    public GameObject itemToDropPrefab3;

    public float itemDropSuccessChance = 0.2f;

    private bool isInvincible = false;

    public bool poisoned = false;
    public float poisonTimerfull = 0.5f;
    public float PoisonTick = 10f;
    public float invincibilityDuration = 0.5f;
    public SpriteRenderer mySprite;

    public bool onFire = false;
    public float FireTimerFull = 1f;
    public float FireTick = 5f;

    private PlayerDataInitializer playerDataInitializer;


    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentState = EnemyState.idle;
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();

    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

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

    IEnumerator InvincibilityFrames()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    /* COULD MAKE THIS IN TAKEDAMAGE SCRIPT AS ITS OWN THING MAYBE SOMEHOW IDK
      public void DamageOverTime(float damage, float time)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    */

    void Die()
    {
        bool itemDropSuccess = Random.Range(0f, 1f) <= itemDropSuccessChance;
        if (itemDropSuccess)
        {
           float randomChance = Random.Range(0f, 1f);
            if (randomChance < 0.5f)
            {
                Debug.Log("Item drop successful, dropping item 1");
                Instantiate(itemToDropPrefab1, transform.position, Quaternion.identity);
            }
            else if (randomChance < 0.8f)
            {
                Debug.Log("Item drop successful, dropping item 2");
                Instantiate(itemToDropPrefab2, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("Item drop successful, dropping item 3");
                Instantiate(itemToDropPrefab3, transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("Item drop failed.");
        }
        // Check if this enemy is the PumpkinKing by comparing its name
        if (gameObject.name == "PumpkinKing")
        {
            // Access the PlayerDataInitializer and update the boss state
            PlayerDataInitializer playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();
            if (playerDataInitializer != null)
            {
                playerDataInitializer.BossDeadList(gameObject.name);
            }

            if (itemToDropPrefab != null)
            {
                Instantiate(itemToDropPrefab, transform.position, Quaternion.identity);

            }
            if (nextSceneObject != null)
            {
                nextSceneObject.SetActive(true);
            }
            if (caveSceneObject != null)
            {
                caveSceneObject.SetActive(true);
            }
            if (BlockadeSceneObject != null)
            {
                BlockadeSceneObject.SetActive(false);
            }

        }

        if (gameObject.name == "LIzardKing")
        {
            // Access the PlayerDataInitializer and update the boss state
            PlayerDataInitializer playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();
            if (playerDataInitializer != null)
            {
                playerDataInitializer.BossDeadList(gameObject.name);
            }

            if (itemToDropPrefab != null)
            {
                Instantiate(itemToDropPrefab, transform.position, Quaternion.identity);

            }
            if (nextSceneObject != null)
            {
                nextSceneObject.SetActive(true);
            }
            if (caveSceneObject != null)
            {
                caveSceneObject.SetActive(true);
            }
            if (BlockadeSceneObject != null)
            {
                BlockadeSceneObject.SetActive(false);
            }

        }

        Debug.Log("My name is: " + gameObject.name);
        PlayerPrefs.SetInt(gameObject.name + "_isEnemyDead", 1);
        PlayerPrefs.Save();
        gameObject.SetActive(false);
       CaveGenerator.Instance.EnemyKilled();
    }




    public void Knock(Rigidbody2D myrigidbod, float KnockTime, float damage)
    {
        StartCoroutine(KnockCo(myrigidbod, KnockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D myrigidbod, float KnockTime)
    {
        if (myrigidbod != null)
        {
            yield return new WaitForSeconds(KnockTime);
            myrigidbod.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myrigidbod.velocity = Vector2.zero;
        }
    }

}

