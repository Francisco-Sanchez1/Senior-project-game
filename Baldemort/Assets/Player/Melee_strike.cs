using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_strike : MonoBehaviour
{
    public float damage = 15f;
    private Weapon_Parent weaponParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        if (hitInfo.gameObject.CompareTag("Player") || (hitInfo.gameObject.CompareTag("Attack")))
        {
            return;
        }
        //Destroy(effect, 5f);
        //Destroy(gameObject);
    }
}
