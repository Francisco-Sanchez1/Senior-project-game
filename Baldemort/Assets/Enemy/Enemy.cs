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

