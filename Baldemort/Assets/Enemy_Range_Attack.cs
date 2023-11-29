using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Range_Attack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackSpeed = 10f;
    public Transform target;
    public bool attacked = false;
    private Vector2 attackDirection;
    private Enemy enemy;
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
        if (enemy.currentState == EnemyState.attackRange && !isCooldown && !attacked)
        {

            // Calculate the rotation angle for the projectile based on the attack direction
            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
            attacked = true;
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = attackDirection * attackSpeed;
            Destroy(projectile, 2f);

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
