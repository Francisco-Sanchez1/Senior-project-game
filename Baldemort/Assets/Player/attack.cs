using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float attackSpeed = 5f;
    public float manaCost = 5f;
    
    

    private Vector2 attackDirection = Vector2.right; // default direction
    private PlayerCntrl player;

    private void Start()
    {
        player = GetComponent<PlayerCntrl>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAttackDirection();
        HandleAttack(); 
        
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

    void HandleAttack()
    {
        //right click to attack
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Check if player has enough mana to attack
            if(player.currentMana >= manaCost)
            {
        // Calculate the rotation angle for the projectile based on the attack direction
            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            Quaternion projectileRotation = Quaternion.Euler(0, 0, angle);

            GameObject projectile = Instantiate(projectilePrefab, transform.position, projectileRotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = attackDirection * attackSpeed;
            Destroy(projectile, 2f);

            // Use mana from PlayerCntrl script
                player.useMana(manaCost);
            }
            else
            {
                Debug.Log("Not enough mana to attack.");
            }
        }
    }
}