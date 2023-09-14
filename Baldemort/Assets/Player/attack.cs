using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackSpeed = 20f;
    public float manaCost = 5f;
    public float damage = 10f;
    

    public float cooldownTime = 2f; // Time in seconds for the attack cooldown
    public bool useCooldown = true; // If true, the cooldown system is used

    private Vector2 attackDirection;
    private PlayerCntrl player;
    private float nextAttackTime = 0f; // Used to keep track of when the player can attack again

    private void Start()
    {
        player = GetComponent<PlayerCntrl>();
    }

    void Update()
    {
        GetAttackDirection();
        HandleAttack();
    }

    void GetAttackDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        attackDirection = (mousePosition - (Vector2)transform.position).normalized;
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Check if cooldown has elapsed (or if we're not using cooldown) and player has enough mana
            if ((!useCooldown || Time.time >= nextAttackTime) && player.currentMana >= manaCost)
            {
                // Calculate the rotation angle for the projectile based on the attack direction
                float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = attackDirection * attackSpeed;
                Destroy(projectile, 2f);

                player.useMana(manaCost);

                if (useCooldown)
                {
                    nextAttackTime = Time.time + cooldownTime;
                }
            }
            else if (Time.time < nextAttackTime)
            {
                Debug.Log("Attack is on cooldown.");
            }
            else
            {
                Debug.Log("Not enough mana to attack.");
            }
        }
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (hitInfo.gameObject.CompareTag("Player") || (hitInfo.gameObject.CompareTag("Attack")))
        {
            return;
        }
        Destroy(gameObject);
    }

}