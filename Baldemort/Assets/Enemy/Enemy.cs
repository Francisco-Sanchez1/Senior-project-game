using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger,
    summon,
    attackRange
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float maxHealth;
    public float currentHealth;
    public HealthBar healthBar;

    public int baseAttack;

    public GameObject itemToDropPrefab;


    public GameObject itemToDropPrefab1;
    public GameObject itemToDropPrefab2;
    public GameObject itemToDropPrefab3;

    public float itemDropSuccessChance = 0.2f;


    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        currentState = EnemyState.idle;
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
            if (itemToDropPrefab != null)
            {
                Instantiate(itemToDropPrefab, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
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

