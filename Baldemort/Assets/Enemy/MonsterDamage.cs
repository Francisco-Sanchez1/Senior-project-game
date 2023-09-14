using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public int damage;
    public PlayerCntrl Health;
    public GameObject player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerCntrl>().takeDamage(damage);
        }
    }

}
