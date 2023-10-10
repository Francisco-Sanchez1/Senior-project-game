using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle,
    inv
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
    
    //Inventory Stuff
    private bool isOpen;
    private Inventory myInventory = new Inventory(18);
    public Item[] itemsToAdd;


    public PlayerState currentState;


    public Color FlashColor;
    public Color regularColor;
    public float flashDur;
    public int numFlash;
    public Collider2D triggerCol;
    public SpriteRenderer mySprite;

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
        currentState = PlayerState.walk;
        

        // Inventory
        foreach(Item item in itemsToAdd)
        {
            myInventory.addItem(new ItemStack(item, 1));
        }


    }



    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprintSpeed : movSpeed;
        movement = new Vector2(horizontalInput, verticalInput).normalized * currentSpeed;
        
        // INVENTORY
         if(Input.GetKeyDown(KeyCode.I))
        {
            if(!isOpen)
            {
                InventoryManager.INSTANCE.openContainer(new ContainerPlayerInventory(null, myInventory));
                isOpen = true;
                Debug.Log("Inventory Opened");
            }
            else
            {
                InventoryManager.INSTANCE.closeContainer();
                isOpen = false;
                Debug.Log("Inventory Closed");
            }
        }

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

            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        StartCoroutine(ToggleInv());

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
        //ToggleInv();
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
            //StartCoroutine(ToggleInv());
            yield return new WaitForSeconds(KnockTime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator ToggleInv()
    {
        int temp = 0;
        //triggerCol.enabled = false;
        //gameObject.layer = LayerMask.NameToLayer("INVINS");
        while (temp < numFlash)
        {
            mySprite.color = FlashColor;
            yield return new WaitForSeconds(flashDur);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDur);
            temp++;
        }

        //gameObject.layer = LayerMask.NameToLayer("Player");
        //triggerCol.enabled = true;
    }

}
