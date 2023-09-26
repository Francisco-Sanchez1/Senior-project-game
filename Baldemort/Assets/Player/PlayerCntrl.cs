using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

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

    public float KnockbackForce = 500f;

    public PlayerState currentState;

    private float inv_frame;
    private float total_frame = 2.0f;

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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inv_frame = 0;
        currentState = PlayerState.walk;
        

    }



    // Update is called once per frame
    void Update()
    {

        inv_frame =  inv_frame - Time.deltaTime;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprintSpeed : movSpeed;
        movement = new Vector2(horizontalInput, verticalInput).normalized * currentSpeed;
        

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(Attackco());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            
            UpdateAnimateAndMove();
        }

        // TESTING PURPOSES REMOVE WHEN ENEMIES IN
        //For Testing Health
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    takeDamage(20f);
        //}

        //For Testing Mana
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //  useMana(20f);
        //}

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


    void UpdateAnimateAndMove()
    {
        if (movement != Vector2.zero)
        {


            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetBool("Move", true);
        }

        else
        {
            animator.SetBool("Move", false);
        }
    }


    public void FixedUpdate()
    {
        //movement

        if(currentState != PlayerState.stagger)
        {
            rb.velocity = movement;
        }

        //rb.AddForce(movement * movSpeed * Time.deltaTime, ForceMode2D.Force);


        //Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //rb.rotation = angle;

    }


    public void takeDamage(float damage)
    {

        if(inv_frame <= 0)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            inv_frame = total_frame;
        }

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


    public void Knock(float KnockTime, float damage)
    {
        StartCoroutine(KnockCo(KnockTime));
        takeDamage(damage);
    }




    private IEnumerator Attackco()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(KnockTime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
    }
}
