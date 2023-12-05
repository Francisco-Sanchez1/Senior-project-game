using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldRange : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;
    public GameObject projectilePrefab3;
    public GameObject projectilePrefab4;
    public float attackSpeed = 6f;
    public Transform target;
    public bool attacked = false;
    private Vector2 attackDirection;
    private Enemy enemy;
    public float decision;
    public bool isCooldown = true; // Control flag for attack state

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
        decision = Random.Range(0f, 1f);

        if (enemy.currentState == EnemyState.attackRange && !isCooldown && !attacked)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            if (decision <= 0.25f) // First quarter range for projectilePrefab
            {
                float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = attackDirection * attackSpeed;
                Destroy(projectile, 2f);
                attacked = true;
            }
            else if (decision <= 0.5f) // Second quarter range for projectilePrefab2
            {
                float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab2, transform.position, projectileRotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = attackDirection * attackSpeed;
                Destroy(projectile, 2f);
                attacked = true;
            }
            else if (decision <= 0.75f) // Third quarter range for projectilePrefab3
            {
                float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab3, transform.position, projectileRotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = attackDirection * attackSpeed;
                Destroy(projectile, 2f);
                attacked = true;
            }
            else // Fourth quarter range for projectilePrefab4
            {
                float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
                Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

                GameObject projectile = Instantiate(projectilePrefab4, transform.position, projectileRotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                rb.velocity = attackDirection * attackSpeed;
                Destroy(projectile, 2f);
                attacked = true;
            }

            StartCoroutine(ResetAttackState());
        }
    }


    IEnumerator ResetAttackState()
    {
        // Wait for a short duration before resetting attack state
        yield return new WaitForSeconds(0.2f);
        attacked = false;
    }
}
