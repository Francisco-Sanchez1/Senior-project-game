using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStrike : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float KnockbackTime;
    public float damage;
    [SerializeField] private string otherTag;
    [SerializeField] private string otherTag2;

    private PhysicsMaterial2D originalPhysicsMaterial;
    private PhysicsMaterial2D temporaryPhysicsMaterial;



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(otherTag) || (collision.gameObject.CompareTag(otherTag2)))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, KnockbackTime, damage);
                }
                if (collision.gameObject.CompareTag("Breakable") && collision.isTrigger)
                {
                    collision.GetComponent<SpawnerDie>().TakeDamage(damage);
                }

                if (collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {
                    hit.GetComponent<PlayerCntrl>().currentState = PlayerState.stagger; 
                    collision.GetComponent<PlayerCntrl>().Knock(KnockbackTime, damage);
                }

            }


        }
    }





}
