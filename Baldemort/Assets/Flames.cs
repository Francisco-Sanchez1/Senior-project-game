using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Flames : MonoBehaviour
{
    public float damage = 5f;
    [SerializeField] private string otherTag;

    private bool hasDealtDamage = false;
    private float invincibilityDuration = 0.5f;


    void Start()
    {
        // Destroy the Flames object after 4 seconds
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDealtDamage && collision.gameObject.CompareTag(otherTag))
        {

            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    Enemy enemy = collision.GetComponent<Enemy>();
                    enemy.fireHurt(damage);
                }
                if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {
                    collision.GetComponent<PlayerCntrl>().takeDamage(damage);
                }
            }
            hasDealtDamage = true;
        }    }


}
