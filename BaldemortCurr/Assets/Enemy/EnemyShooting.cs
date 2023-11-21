using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //public GameObject hitEffect;
    private PlayerCntrl player;
    public float damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        PlayerCntrl player = hitInfo.GetComponent<PlayerCntrl>();
        if (player != null)
        {
            player.takeDamage(damage);
        }
        if (hitInfo.gameObject.CompareTag("Enemy") || (hitInfo.gameObject.CompareTag("Attack")))
        {
            return;
        }
        //Destroy(effect, 5f);
        Destroy(gameObject);
    }



}
