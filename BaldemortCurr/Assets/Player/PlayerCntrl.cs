using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MoreMountains.Tools;
using MoreMountains.InventoryEngine;
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
    public string PlayerID = "Player1";

    //MVMT stuff
    public float movSpeed;
    public float sprintSpeed;

    public Rigidbody2D rb;

    Vector2 movement;
    Vector2 normalizedMovement;

    Vector2 mousePos;

    [SerializeField]
    public HealthBar healthBar;
    public float maxHealth;
    public float currentHealth;
    //ManaStuff
    public float maxMana;
    public float currentMana;
    public ManaBar manaBar;

    public Slider healthSlider;
    public Slider manaSlider;
    //MANA REGEN STUFF
    private float ManaRegenTimer;
    public const float ManaUseTimer = 5.0f;
    private float SlowRegen;
    public const float SlowRegenTimer = 1.0f;

    public Camera cam;

    public Animator animator;

    public float KnockbackForce = 500f;

    // //Inventory Stuff
    // private bool isOpen;
    // private Inventory myInventory = new Inventory(18);
    // public Item[] itemsToAdd;
    
    // //Inventory Stuff
    // the sprite used to show the current weapon
		public SpriteRenderer WeaponSprite;
		/// the armor inventory
		public Inventory ArmorInventory;
		/// the weapon inventory
		public Inventory WeaponInventory;


    public PlayerState currentState;


    public Color FlashColor;
    public Color regularColor;
    public float flashDur;
    public int numFlash;
    public float invTimer;
    public float invTimerFull = 0.50f;

    public Collider2D triggerCol;
    public SpriteRenderer mySprite;
    public int currentSceneIndex;
    public bool isDead = false;
    private PlayerDataInitializer playerDataInitializer;

    //MONEY
    public int coins;
    // Start is called before the first frame update
    void Start()
    {
        playerDataInitializer = FindObjectOfType<PlayerDataInitializer>();

        maxHealth = playerDataInitializer.maxHealth;
        currentHealth = playerDataInitializer.currentHealth;
        healthBar.SetMaxHealth(playerDataInitializer.maxHealth);
        healthBar.SetHealth(playerDataInitializer.currentHealth);

        maxMana = playerDataInitializer.maxMana;
        currentMana = playerDataInitializer.currentMana;
        manaBar.SetMaxMana(playerDataInitializer.maxMana);
        manaBar.SetMana(playerDataInitializer.currentMana);

        coins = playerDataInitializer.coins;

        ManaRegenTimer = ManaUseTimer;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentState = PlayerState.walk;


        // // Inventory
        // foreach (Item item in itemsToAdd)
        // {
        //     myInventory.addItem(new ItemStack(item, 1));
        // }


    }

    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        //Dealing with health,Mana, and coins

        if (DialogManager.isActive == true || ShopKeeperNPC.ShopActive == true)
        {
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            animator.SetBool("Move", false);
            return;
            
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? sprintSpeed : movSpeed;
        movement = new Vector2(horizontalInput, verticalInput).normalized * currentSpeed;
        invTimer = invTimer - Time.deltaTime;

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
        if (playerDataInitializer.currentMana < playerDataInitializer.maxMana)
        {

            ManaRegenTimer = ManaRegenTimer - Time.deltaTime;
            SlowRegen = SlowRegen - Time.deltaTime;
            if (ManaRegenTimer <= 0.0f && SlowRegen <= 0.0f)
            {
                playerDataInitializer.currentMana += 10f;
                currentMana = playerDataInitializer.currentMana;
                manaBar.SetMana(playerDataInitializer.currentMana);
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
        if (DialogManager.isActive == true || ShopKeeperNPC.ShopActive == true)
        {
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            animator.SetBool("Move", false);
            return;

        }
        if (currentState != PlayerState.stagger)
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
        if (invTimer <= 0)
        {

            playerDataInitializer.currentHealth -= damage;
            currentHealth = playerDataInitializer.currentHealth;
            healthBar.SetHealth(playerDataInitializer.currentHealth);
            invTimer = invTimerFull;
            StartCoroutine(ToggleInv());
        }

        if (currentHealth <= 0)
        {
            // Start the death animation coroutine
            StartCoroutine(Death_Anim());
        }
    }


    public void IncreaseHealth(float amount)
    {
        playerDataInitializer.maxHealth += amount;
        maxHealth = playerDataInitializer.maxHealth;

        if (playerDataInitializer.currentHealth <= playerDataInitializer.maxHealth)
        {
            currentHealth = playerDataInitializer.maxHealth;
            playerDataInitializer.currentHealth = playerDataInitializer.maxHealth;
            
        }

        healthBar.SetMaxHealth(playerDataInitializer.maxHealth);
        healthBar.SetHealth(playerDataInitializer.currentHealth);

        healthSlider.maxValue = playerDataInitializer.maxHealth;
        healthSlider.value = playerDataInitializer.currentHealth;
    }

    public void Heart_Pick(float amount)
    {
        playerDataInitializer.currentHealth += amount;
        currentHealth = playerDataInitializer.currentHealth;

        if (playerDataInitializer.currentHealth >= playerDataInitializer.maxHealth)
        {
            playerDataInitializer.currentHealth = playerDataInitializer.maxHealth;
            currentHealth = playerDataInitializer.currentHealth;
        }
        healthBar.SetHealth(playerDataInitializer.currentHealth);

        healthSlider.value = playerDataInitializer.currentHealth;
    }

    public void IncreaseMana(float amount)
    {
        playerDataInitializer.maxMana += amount;
        maxMana = playerDataInitializer.maxMana;
        if (playerDataInitializer.currentMana <= playerDataInitializer.maxMana)
        {
            currentMana = playerDataInitializer.maxMana;
            playerDataInitializer.currentMana = playerDataInitializer.maxMana;
        }

        manaBar.SetMaxMana(playerDataInitializer.maxMana);
        manaBar.SetMana(playerDataInitializer.currentMana);

        // Update the mana slider's max value and current value
        manaSlider.maxValue = playerDataInitializer.maxMana;
        manaSlider.value = playerDataInitializer.currentMana;
    }


    //COIN STUFF
    public void Coin_Pick(int amount)
    {
        playerDataInitializer.coins += amount;
        coins = playerDataInitializer.coins;

    }

    public void SpendCoin(int amount)
    {
        if (playerDataInitializer.coins >= amount)
        {
            playerDataInitializer.coins -= amount;
            coins = playerDataInitializer.coins;

        }
        else
        {
            Debug.Log("Not enough coins to deduct!");
        }
    }

    private IEnumerator Death_Anim()
    {
        // Play the death animation
        animator.SetBool("isDead", true);
        animator.SetBool("attacking", false);
        animator.SetBool("idle", true);

        //set  to kinematic to prevent movement
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        // Optionally, you can disable player controls here
        PlayerCntrl playerController = GetComponent<PlayerCntrl>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // Wait for the animation to finish (adjust the duration as needed)
        yield return new WaitForSeconds(4);

        // Transition to the game over screen
        SceneManager.LoadScene("Death_Screen");
    }

    public void useMana(float manaUse)
    {
        playerDataInitializer.currentMana -= manaUse;
        currentMana = playerDataInitializer.currentMana;
        manaBar.SetMana(playerDataInitializer.currentMana);
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

        // //INVENTOPRY STUFF
		// public virtual void SetArmor(int index)
		// {
		// 	_currentArmor = index;
		// }

		// /// <summary>
		// /// Sets the current weapon sprite
		// /// </summary>
		// /// <param name="newSprite">New sprite.</param>
		// /// <param name="item">Item.</param>
		public virtual void SetWeapon(Sprite newSprite, InventoryItem item)
		{
			WeaponSprite.sprite = newSprite;
            // debug log weapon name 
            Debug.Log(item.name);

		}

        public void RegainMana(float amount)
        {
        playerDataInitializer.currentMana += amount;
            if(playerDataInitializer.currentMana > playerDataInitializer.maxMana)
            {
            playerDataInitializer.currentMana = playerDataInitializer.maxMana; // Ensure mana doesn't exceed max value
            }
            manaBar.SetMana(playerDataInitializer.currentMana);
        }

        /// <summary>
		/// Catches MMInventoryEvents and if it's an "inventory loaded" one, equips the first armor and weapon stored in the corresponding inventories
		/// </summary>
		/// <param name="inventoryEvent">Inventory event.</param>
		// public virtual void OnMMEvent(MMInventoryEvent inventoryEvent)
		// {
		// 	if (inventoryEvent.InventoryEventType == MMInventoryEventType.InventoryLoaded)
		// 	{
		// 		// if (inventoryEvent.TargetInventoryName == "RogueArmorInventory")
		// 		// {
		// 		// 	if (ArmorInventory != null)
		// 		// 	{
		// 		// 		if (!InventoryItem.IsNull(ArmorInventory.Content [0]))
		// 		// 		{
		// 		// 			ArmorInventory.Content [0].Equip (PlayerID);	
		// 		// 		}
		// 		// 	}
		// 		// }
		// 		if (inventoryEvent.TargetInventoryName == "RogueWeaponInventory")
		// 		{
		// 			if (WeaponInventory != null)
		// 			{
		// 				if (!InventoryItem.IsNull (WeaponInventory.Content [0]))
		// 				{
		// 					WeaponInventory.Content [0].Equip (PlayerID);
		// 				}
		// 			}
		// 		}
		// 	}
		// }

	}
