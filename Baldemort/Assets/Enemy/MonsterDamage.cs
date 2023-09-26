using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public PlayerCntrl Health;
    public GameObject player;
    public float KnockbackForce;
    Rigidbody2D rb;
    public float Wait_Damage = 1f;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Wait_Damage -= Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collision.gameObject.tag == "Player")
        {
            
            if(Wait_Damage <= 0)
            {
                Vector2 difference = (collider.transform.position - transform.position).normalized;
                Vector2 Knock = difference * KnockbackForce;
                rb.AddForce(Knock, ForceMode2D.Impulse);

                transform.position = transform.position;
                collision.gameObject.GetComponent<PlayerCntrl>().takeDamage(damage);


                Wait_Damage = 2f;
            }

        }

    }




}
