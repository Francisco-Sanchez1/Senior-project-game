using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_Arrow: MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackSpeed = 10f;
    public Transform target;

    public float cooldownTime = .5f; // Time in seconds for the attack cooldown
    public bool useCooldown = true; // If true, the cooldown system is used

    private Vector2 attackDirection;
    private Enemy enemy;
    private float nextAttackTime = 0f; // Used to keep track of when the player can attack again

    

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        GetAttackDirection();
        HandleAttack();
    }

    void GetAttackDirection()
    {
        Vector2 temp = Vector3.MoveTowards(transform.position, target.position, attackSpeed * Time.deltaTime);
        attackDirection = (temp - (Vector2)transform.position).normalized;
    }

    void HandleAttack()
    {
        if (enemy.currentState == EnemyState.attack)
        {
            // Check if cooldown has elapsed (or if we're not using cooldown) and player has enough mana
            if (!useCooldown || Time.time >= nextAttackTime)
            {
                // Calculate the rotation angle for the projectile based on the attack direction
                float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = attackDirection * attackSpeed;
                Destroy(projectile, 2f);

                if (useCooldown)
                {
                    nextAttackTime = Time.time + cooldownTime;
                }
            }

        }
    }




}