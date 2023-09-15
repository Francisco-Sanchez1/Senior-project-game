using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerCntrl : MonoBehaviour
{

    //MVMT stuff
    public float movSpeed;
    public float sprintSpeed;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 normalizedMovement;

    Vector2 mousePos;

    //HealthStuff
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar;

    //ManaStuff
    public float maxMana = 100f;
    public float currentMana;
    public ManaBar manaBar;

    //MANA REGEN STUFF
    private float ManaRegenTimer;
    public const float ManaUseTimer = 5.0f;
    private float SlowRegen;
    public const float SlowRegenTimer = 1.0f;

    public Camera cam;

    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {

        //HealthStuff
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        //ManaStuff
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);

        ManaRegenTimer = ManaUseTimer;



    }



    // Update is called once per frame
    void Update()
    {
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprintSpeed : movSpeed;
        


        //Input from user
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Store the input in a member variable to be used in FixedUpdate
        movement = new Vector2(horizontalInput, verticalInput).normalized * currentSpeed;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
   



        // TESTING PURPOSES REMOVE WHEN ENEMIES IN
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

        //MANA_REGEN TESTING
        if (currentMana < maxMana)
        {

            ManaRegenTimer = ManaRegenTimer - Time.deltaTime;
            SlowRegen = SlowRegen - Time.deltaTime;
            if (ManaRegenTimer <= 0.0f && SlowRegen <= 0.0f)
            {
                currentMana += 10f;
                manaBar.SetMana(currentMana);
                SlowRegen = SlowRegenTimer;

            }
        }
        
        

    }



    private Vector2 GetPointer()
    {
        Vector3 mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }


    public void FixedUpdate()
    {
        //movement
        

        rb.velocity = movement;




        //Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
       //rb.rotation = angle;

    }


    private void AnimateChar()
    {
        Vector2 lookDirection = mousePos - (Vector2)transform.position;
        
        
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void useMana(float manaUse)
    {
        currentMana -= manaUse;
        manaBar.SetMana(currentMana);
        ManaRegenTimer = ManaUseTimer;

    }


}
