using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour
{

    //MVMT stuff
    public float movSpeed; 
    
    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 normalizedMovement;

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
        //Input from user
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        


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

    public void FixedUpdate()
    {
        //movement
        Vector2 normalizedMovement = movement.normalized;
        Vector2 velocity = normalizedMovement * movSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + velocity);
        
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
