using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{

    public float movSpeed; 
    float speedX, speedY;
    Rigidbody2D rb;

    //HealthStuff
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;

    //ManaStuff
    public float maxMana = 100f;
    public float currentMana;
    public ManaBar manaBar;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //HealthStuff
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //ManaStuff
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana); 

    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        Vector2 movement = new Vector2(speedX, speedY);
        movement.Normalize();

        rb.velocity = movement * movSpeed;




        //For Testing Health
        if (Input.GetKeyDown(KeyCode.E))
        {
            takeDamage(20f);
        }

        //For Testing Mana
        if (Input.GetKeyDown(KeyCode.F))
        {
            useMana(20f);
        }

    }


    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

    }

    public void useMana(float manaUse)
    {
        currentMana -= manaUse;
        manaBar.SetMana(currentMana);
    }
}
