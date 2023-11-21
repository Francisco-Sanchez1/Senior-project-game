using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon_Parent : MonoBehaviour
{

    public GameObject MeleePrefab;
    public float attackSpeed = 5f;

    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public Animator animator;

    public float delay = 2f;
    public bool AttackLock;
    public Transform Weapon_Attack;
    public GameObject wandPrefab;
    public float damage = 5f;

    public bool isAttacking;
    public Transform circleOrigin;
    public float radius;

    private Vector2 attackDirection = Vector2.right; // default direction
    private PlayerCntrl player;


    public void ResetIsAttacking()
    {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetAttackDirection();

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack();
            isAttacking = true;

        }
    }


    void GetAttackDirection()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            attackDirection += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            attackDirection += Vector2.down;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            attackDirection += Vector2.left;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            attackDirection += Vector2.right;
        }

        if (attackDirection != Vector2.zero)
        {
            attackDirection.Normalize();
        }
    }


 


    public void Attack()
    {

        Rigidbody2D rb = Weapon_Attack.GetComponent<Rigidbody2D>();
        
        if (AttackLock)
        {
            return;

        }
        animator.SetTrigger("Attack");
        AttackLock = true;
        StartCoroutine(DelayAttack());

    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        AttackLock = false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }


    public void DetectCollide(Collider2D hitInfo)
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            Debug.Log(collider.name);
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            if (hitInfo.gameObject.CompareTag("Player") || (hitInfo.gameObject.CompareTag("Attack")))
            {
                return;
            }
        }
    }


}
